using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

using Halo5Reqs.Api;
using Halo5Reqs.Properties;

namespace Halo5Reqs
{
	public partial class MainForm : Form
	{
		private const int LVIS_CUT = 4;
		private const int LVIS_OVERLAYMASK = 0xF00;

		[DllImport("ComCtl32.dll")]
		private static extern int ImageList_SetOverlayImage(IntPtr himl, int iImage, int iOverlay);

		private static readonly MethodInfo s_listViewSetItemState =
			typeof(ListView).GetMethod("SetItemState", BindingFlags.NonPublic | BindingFlags.Instance);

		private readonly ILogger _logger;

		private readonly Halo5ApiClient _halo5ApiClient;
		private readonly PacksApiClient _packsApiClient;

		private readonly Settings _settings = Settings.Default;

		private List<PackType> _packTypes;
		private List<PackType> _storePackTypes;
		private List<Req> _reqs;
		private Dictionary<String, Req> _reqsById;
		private IDictionary<String, Card> _cardsByReqId;

		private PackType _selectedPackType;
		private PackInstance _selectedPackInstance;

		private ReqCategory _selectedCategory;
		private ReqSubCategory _selectedSubCategory;
		private Rarity _selectedRarity;

		private Req _selectedReq;
		private CardInstance _selectedCardInstance;

		private Queue<(Req, ListViewItem)> _reqImageQueue;

		public MainForm(ILogger logger, String token, String gamertag)
		{
			_logger = logger;

			_halo5ApiClient = new Halo5ApiClient(token);
			_packsApiClient = new PacksApiClient(token, gamertag, _halo5ApiClient);

			this.InitializeComponent();
			this.Text = String.Format(this.Text, gamertag);
		}

		private async void MainForm_Load(Object sender, EventArgs e)
		{
			_packTypes = (await _packsApiClient.GetPacksAsync()).ToList();
			_storePackTypes = (await _packsApiClient.GetStorePacksAsync()).ToList();
			_reqs = (await _halo5ApiClient.GetReqsAsync()).ToList();
			_reqsById = _reqs.ToDictionary(req => req.Id);
			_cardsByReqId = (await _packsApiClient.GetCardsAsync())
				.Where(card => card.Unconsumed > 0)
				.ToDictionary(card => card.ReqId);

			this.CreateOverlayImages();
			this.PopulatePackTreeView(null);
			this.PopulateReqTreeView();
		}

		private void tabControl_SelectedIndexChanged(Object sender, EventArgs e)
		{
			this.ShowReq(null, null);
		}

		private async void packTreeView_AfterSelect(Object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag is PackType packType)
			{
				_selectedPackType = packType;
				_selectedPackInstance = null;

				this.packTypePictureBox.Image = await _selectedPackType.GetImageAsync();
				this.openPackButton.Visible = false;
				this.buyPackButton.Visible = _selectedPackType.CreditPrice != null;
			}
			else if (e.Node.Tag is PackInstance packInstance)
			{
				_selectedPackType = (PackType)e.Node.Parent.Tag;
				_selectedPackInstance = packInstance;

				this.packTypePictureBox.Image = await _selectedPackType.GetImageAsync();
				this.openPackButton.Enabled = packInstance.CanBeOpened;
				this.openPackButton.Visible = true;
				this.buyPackButton.Visible = false;
			}
			else
			{
				_selectedPackType = null;
				_selectedPackInstance = null;

				this.packTypePictureBox.Image = null;
				this.openPackButton.Visible = false;
				this.buyPackButton.Visible = false;
			}

			this.PopulatePackListView();
		}

		private async void openPackButton_Click(Object sender, EventArgs e)
		{
			PackType packType = _selectedPackType;
			PackInstance packInstance = _selectedPackInstance;

			_logger.Log($"Opening {packType.Name} ({packType.Id}/{packInstance.Id})");

			this.packTreeView.SelectedNode.Text += " (opened)";
			packInstance.CanBeOpened = false;
			this.openPackButton.Enabled = false;
			await _packsApiClient.OpenPackAsync(packType, packInstance);

			foreach (CardInstance cardInstance in packInstance.CardInstances)
			{
				String reqId = cardInstance.ReqId;
				_logger.Log($"  {_reqsById[reqId].Name} ({reqId}/{cardInstance.Id})");

				if (!cardInstance.Consumed)
				{
					if (_cardsByReqId.TryGetValue(reqId, out Card card))
					{
						card.Unconsumed++;
					}
					else
					{
						card = new Card(reqId, 1);
						_cardsByReqId.Add(reqId, card);
					}
				}
			}

			_logger.Log("");

			this.PopulatePackListView();
			this.PopulateReqTreeView(); // Update counts.
			this.reqTreeView.SelectedNode = this.reqTreeView.Nodes[0];
		}

		private async void buyPackButton_Click(Object sender, EventArgs e)
		{
			String packInstanceId = await _packsApiClient.BuyPackAsync(_selectedPackType);
			this.PopulatePackTreeView(packInstanceId);
		}

		private void packListView_KeyDown(Object sender, KeyEventArgs e)
		{
			if (this.packListView.SelectedItems.Count > 0 &&
				this.packListView.SelectedItems[0].Tag == _selectedCardInstance &&
				e.KeyCode == Keys.Delete)
			{
				this.sellButton.PerformClick();
			}
		}

		private void packListView_SelectedIndexChanged(Object sender, EventArgs e)
		{
			if (this.packListView.SelectedItems.Count > 0)
			{
				CardInstance cardInstance = (CardInstance)this.packListView.SelectedItems[0].Tag;
				this.ShowReq(_reqsById[cardInstance.ReqId], cardInstance);
			}
			else
			{
				this.ShowReq(null, null);
			}
		}

		private void reqTreeView_AfterSelect(Object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag is ReqSubCategory subCategory)
			{
				_selectedCategory = (ReqCategory)e.Node.Parent.Tag;
				_selectedSubCategory = subCategory;
				_selectedRarity = null;
			}
			else if (e.Node.Tag is Rarity rarity)
			{
				_selectedCategory = (ReqCategory)e.Node.Parent.Parent.Tag;
				_selectedSubCategory = (ReqSubCategory)e.Node.Parent.Tag;
				_selectedRarity = rarity;
			}
			else
			{
				_selectedCategory = null;
				_selectedSubCategory = null;
				_selectedRarity = null;
			}

			this.PopulateReqListView();
		}

		private void reqListView_KeyDown(Object sender, KeyEventArgs e)
		{
			if (this.reqListView.SelectedItems.Count > 0 &&
				this.reqListView.SelectedItems[0].Tag == _selectedReq &&
				e.KeyCode == Keys.Delete)
			{
				this.sellButton.PerformClick();
			}
		}

		private void reqListView_SelectedIndexChanged(Object sender, EventArgs e)
		{
			Req req = this.reqListView.SelectedItems.Count > 0
				? (Req)this.reqListView.SelectedItems[0].Tag
				: null;
			this.ShowReq(req, null);
		}

		private async void sellButton_Click(Object sender, EventArgs e)
		{
			Req req = _selectedReq;

			if (MessageBox.Show(this, $"Are you sure you want to sell one of {req.Name}?", "Sell 1 Card", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}

			_logger.Log($"Selling {req.Name} ({req.Id})");
			this.sellButton.Enabled = false;
			Card card = _cardsByReqId[req.Id];
			PackInstance packInstance = _selectedCardInstance != null ? _selectedPackInstance : null;
			CardInstance cardInstance = _selectedCardInstance;
			String cardInstanceId = await _packsApiClient.SellCardAsync(
				req, card, cardInstance?.Id, _settings.MinReqCount);

			if (cardInstanceId != null) // Was a card actually sold?
			{
				_logger.Log($"  Instance {cardInstanceId}");

				if (cardInstance == null)
				{
					// Check whether the card instance is from a recently opened pack:
					(PackInstance, CardInstance)? tuple = this.FindCardInstance(cardInstanceId);

					if (tuple != null)
					{
						(packInstance, cardInstance) = tuple.Value;
					}
				}

				if (cardInstance != null)
				{
					cardInstance.Consumed = true;
				}
			}
			else
			{
				_logger.Log("  NO CARD SOLD");
			}

			if (card.Unconsumed > 0)
			{
				this.ShowUnconsumed(card);
			}
			else
			{
				_cardsByReqId.Remove(req.Id);
				this.ShowUnconsumed(null);
			}

			if (packInstance != null && packInstance == _selectedPackInstance)
			{
				foreach (ListViewItem item in this.packListView.Items)
				{
					if (item.Tag == cardInstance)
					{
						this.SetOverlayImage(this.packListView, item, 0);
						this.SetCutStyle(this.packListView, item);
						break;
					}
				}
			}
			else if (card.Unconsumed <= _settings.MinReqCount &&
				this.packListView.Items.Cast<ListViewItem>().Any(item => ((CardInstance)item.Tag).ReqId == req.Id))
			{
				this.PopulatePackListView(); // Update the unconsumed status of all equivalent reqs.
			}

			if (card.Unconsumed <= _settings.MinReqCount)
			{
				foreach (ListViewItem item in this.reqListView.Items)
				{
					if (item.Tag == req)
					{
						if (card.Unconsumed > 0)
						{
							this.SetOverlayImage(this.reqListView, item, 0);
						}
						else
						{
							this.SetCutStyle(this.reqListView, item);
						}

						break;
					}
				}
			}
		}

		private (PackInstance packInstance, CardInstance cardInstance)? FindCardInstance(String cardInstanceId)
		{
			foreach (PackInstance packInstance in _packTypes
				.SelectMany(packType => packType.Instances)
				.Where(packInstance => packInstance.CardInstances != null))
			{
				CardInstance cardInstance = packInstance.CardInstances
					.FirstOrDefault(cardInstance_ => cardInstance_.Id == cardInstanceId);

				if (cardInstance != null)
				{
					return (packInstance, cardInstance);
				}
			}

			return null;
		}

		private void CreateOverlayImages()
		{
			Font font = this.reqListView.Font;
			Size bitmapSize = this.reqImageList.ImageSize;

			for (Int32 index = 2; index <= 10; index++)
			{
				String text = index < 10 ? $"x{index}" : "x9+";
				Bitmap bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);

				using (Graphics g = Graphics.FromImage(bitmap))
				{
					SizeF textSize = g.MeasureString(text, font) + new SizeF(2, 0);
					Single x = bitmapSize.Width - textSize.Width - 2;
					Single y = bitmapSize.Height - textSize.Height - 2;
					g.DrawRectangle(Pens.Black, x, y, textSize.Width + 1, textSize.Height + 1);
					g.FillRectangle(Brushes.Gold, x + 1, y + 1, textSize.Width, textSize.Height);
					g.DrawString(text, font, Brushes.Black, x + 2, y + 1);
				}

				this.reqImageList.Images.Add(text, bitmap);
				ImageList_SetOverlayImage(this.reqImageList.Handle, this.reqImageList.Images.Count - 1, index);
			}
		}

		private void PopulatePackTreeView(String selectedPackInstanceId)
		{
			this.packTreeView.Nodes.Clear();
			TreeNode ownedNode = this.packTreeView.Nodes.Add("My Packs");

			foreach (PackType packType in _packTypes)
			{
				TreeNode packTypeNode = ownedNode.Nodes.Add(packType.ToString());
				packTypeNode.Tag = packType;

				foreach (PackInstance packInstance in packType.Instances
					.OrderBy(packInstance => packInstance.AcquiredDateTime))
				{
					TreeNode packInstanceNode = packTypeNode.Nodes.Add(packInstance.ToString());
					packInstanceNode.Tag = packInstance;

					if (packInstance.Id == selectedPackInstanceId)
					{
						this.packTreeView.SelectedNode = packInstanceNode;
					}
				}
			}

#if false
			TreeNode storeNode = this.packTreeView.Nodes.Add("Store");

			foreach (PackType packType in _storePackTypes)
			{
				TreeNode storePackType = storeNode.Nodes.Add($"{packType.Name} - {packType.CreditPrice} Req Points");
				storePackType.Tag = packType;
			}
#endif

			this.packTreeView.ExpandAll();
		}

		private void PopulatePackListView()
		{
			if (_reqImageQueue != null)
			{
				_reqImageQueue.Clear();
			}

			this.packListView.Items.Clear();

			if (_selectedPackInstance != null && _selectedPackInstance.CardInstances != null)
			{
				Boolean createdReqQueue = false;
				this.packListView.BeginUpdate();

				foreach (CardInstance cardInstance in _selectedPackInstance.CardInstances)
				{
					Req req = _reqs.Single(r => r.Id == cardInstance.ReqId);
					ListViewItem item = this.packListView.Items.Add(req.Name);
					item.Tag = cardInstance;
					item.SubItems.Add(req.Rarity.Id);

					if (cardInstance.Consumed)
					{
						if (req.SellPrice != null)
						{
							this.SetCutStyle(this.packListView, item);
						}
					}
					else
					{
						Card card = _cardsByReqId[req.Id];

						if (_settings.MinReqCount > 0 && card.Unconsumed > _settings.MinReqCount)
						{
							this.SetOverlayImage(this.packListView, item, Math.Min(card.Unconsumed, 10));
						}
					}

					if (this.reqImageList.Images.ContainsKey(req.Id))
					{
						item.ImageKey = req.Id;
					}
					else
					{
						if (_reqImageQueue == null)
						{
							_reqImageQueue = new Queue<(Req, ListViewItem)>();
							createdReqQueue = true;
						}

						_reqImageQueue.Enqueue((req, item));
					}
				}

				this.packListView.EndUpdate();

				if (createdReqQueue)
				{
					this.LoadReqImages();
				}
			}

			this.ShowReq(null, null);
		}

		private void PopulateReqTreeView()
		{
			this.reqTreeView.Nodes.Clear();

			ILookup<ReqCategory, Req> reqsByCategory = _reqs.ToLookup(req => req.Category);

			foreach (ReqCategory category in ReqCategory.AllCategories.Where(category => category.Id != "Other"))
			{
				IEnumerable<Req> categoryReqs = reqsByCategory[category];
				TreeNode categoryNode = this.reqTreeView.Nodes.Add(category.Name);
				categoryNode.Tag = category;

				ILookup<ReqSubCategory, Req> reqsBySubCategory = categoryReqs.ToLookup(req => req.SubCategory);

				foreach (ReqSubCategory subCategory in category.SubCategories)
				{
					IEnumerable<Req> subCategoryReqs = reqsBySubCategory[subCategory];
					TreeNode subCategoryNode = categoryNode.Nodes.Add($"{subCategory} {this.GetReqCounts(subCategoryReqs)}");
					subCategoryNode.Tag = subCategory;

					foreach (IGrouping<Rarity, Req> reqGrouping in subCategoryReqs
						.GroupBy(req => req.Rarity)
						.OrderBy(grouping => grouping.Key.Order))
					{
						Rarity rarity = reqGrouping.Key;
						TreeNode rarityNode = subCategoryNode.Nodes.Add($"{rarity} {this.GetReqCounts(reqGrouping)}");
						rarityNode.Tag = rarity;
					}
				}

				categoryNode.Expand();
			}
		}

		private String GetReqCounts(IEnumerable<Req> reqs)
		{
			Int32 totalReqCount = reqs.Count();
			Int32 unconsumedReqCount = reqs.Count(req => _cardsByReqId.ContainsKey(req.Id));
			return $"({unconsumedReqCount}/{totalReqCount})";
		}

		private void PopulateReqListView()
		{
			if (_reqImageQueue != null)
			{
				_reqImageQueue.Clear();
			}

			this.reqListView.Items.Clear();

			if (_selectedSubCategory != null)
			{
				IEnumerable<Req> reqs = _reqs.Where(req => req.SubCategory == _selectedSubCategory);
				if (_selectedRarity != null)
					reqs = reqs.Where(req => req.Rarity == _selectedRarity);
				IOrderedEnumerable<Req> orderedReqs = _selectedCategory.Id == "PowerAndVehicle"
					? reqs.OrderBy(req => req.CategoryDisplayName).ThenBy(req => req.Rarity.Order)
					: reqs.OrderBy(req => req.Rarity.Order);
				orderedReqs = orderedReqs.ThenBy(req => req.Name);
				if (_selectedCategory.Id == "Loadout")
					orderedReqs = orderedReqs.ThenBy(req => req.LevelRequirement);

				Boolean createdReqQueue = false;
				this.reqListView.BeginUpdate();

				foreach (Req req in orderedReqs)
				{
					ListViewItem item = this.reqListView.Items.Add(req.Name);
					item.Tag = req;
					item.SubItems.Add(req.Rarity.Id);

					if (_cardsByReqId.TryGetValue(req.Id, out Card card))
					{
						if (_settings.MinReqCount > 0 && card.Unconsumed > _settings.MinReqCount)
						{
							this.SetOverlayImage(this.reqListView, item, Math.Min(card.Unconsumed, 10));
						}
					}
					else
					{
						this.SetCutStyle(this.reqListView, item);
					}

					if (this.reqImageList.Images.ContainsKey(req.Id))
					{
						item.ImageKey = req.Id;
					}
					else
					{
						if (_reqImageQueue == null)
						{
							_reqImageQueue = new Queue<(Req, ListViewItem)>();
							createdReqQueue = true;
						}

						_reqImageQueue.Enqueue((req, item));
					}
				}

				this.reqListView.EndUpdate();

				if (createdReqQueue)
				{
					this.LoadReqImages();
				}
			}

			this.ShowReq(null, null);
		}

		private async void LoadReqImages()
		{
			while (_reqImageQueue.Count > 0)
			{
				(Req req, ListViewItem item) = _reqImageQueue.Dequeue();
				Image image = await req.GetImageAsync();

				if (!this.reqImageList.Images.ContainsKey(req.Id))
				{
					this.reqImageList.Images.Add(req.Id, image);
				}

				item.ImageKey = req.Id;
			}

			_reqImageQueue = null;
		}

		private async void ShowReq(Req req, CardInstance cardInstance)
		{
			_selectedReq = req;
			_selectedCardInstance = cardInstance;

			if (req != null)
			{
				this.reqNameLabel.Text = req.Name;
				this.reqDescriptionLabel.Text = req.Description;

				if (req.SellPrice == null)
				{
					this.priceLabel.Visible = false;
					this.priceLabel2.Visible = false;
					this.unconsumedLabel.Visible = false;
					this.unconsumedLabel2.Visible = false;
					this.sellButton.Visible = false;
				}
				else
				{
					this.priceLabel.Text = req.SellPrice.ToString();
					this.priceLabel.Visible = true;
					this.priceLabel2.Visible = true;

					if (_cardsByReqId.TryGetValue(req.Id, out Card card))
					{
						this.unconsumedLabel.Text = card.Unconsumed.ToString();
						this.unconsumedLabel.Visible = true;
						this.unconsumedLabel2.Visible = true;
						this.sellButton.Visible = true;
						this.sellButton.Enabled = card.Unconsumed > _settings.MinReqCount;
					}
					else
					{
						this.unconsumedLabel.Visible = false;
						this.unconsumedLabel2.Visible = false;
						this.sellButton.Visible = false;
					}
				}

				this.reqPanel.Visible = true;
				this.reqPictureBox.Image = await req.GetImageAsync();
			}
			else
			{
				this.reqPanel.Visible = false;
			}
		}

		private void ShowUnconsumed(Card card)
		{
			if (card != null)
			{
				this.unconsumedLabel.Text = card.Unconsumed.ToString();
				this.unconsumedLabel.Visible = true;
				this.unconsumedLabel2.Visible = true;
				this.sellButton.Visible = true;
				this.sellButton.Enabled = card.Unconsumed > _settings.MinReqCount;
			}
			else
			{
				this.unconsumedLabel.Visible = false;
				this.unconsumedLabel2.Visible = false;
				this.sellButton.Visible = false;
			}
		}

		private void SetCutStyle(ListView listView, ListViewItem item)
		{
			item.ForeColor = Color.Gray;
			s_listViewSetItemState.Invoke(listView, new object[] { item.Index, LVIS_CUT, LVIS_CUT });
		}

		private void SetOverlayImage(ListView listView, ListViewItem item, Int32 imageIndex)
		{
			s_listViewSetItemState.Invoke(listView, new object[] { item.Index, imageIndex << 8, LVIS_OVERLAYMASK });
		}
	}
}
