using PhasmophobiaCompanion.Models;
using PhasmophobiaCompanion.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace PhasmophobiaCompanion.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GhostPage : ContentPage
    {
        List<Ghost> ghosts;
        public GhostPage()
        {
            InitializeComponent();
            ghosts = new List<Ghost>()
            {
                new Ghost()
                {
                    Clues=new List<CluesStructure>
                    {
                        new CluesStructure { Name="Book", Source="Book_icon.png"},
                        new CluesStructure { Name = "Radio", Source = "Radio_icon.png" },
                        new CluesStructure { Name = "Minus", Source = "Minus_icon.png" }
                    },
                    ImageUrl="Moroi.jpg",
                    Title="Moroi"
                },
                new Ghost()
                {
                    Clues=new List<CluesStructure>
                    {
                        new CluesStructure { Name="Radio", Source="Radio_icon.png"},
                        new CluesStructure { Name = "Book", Source = "Book_icon.png" },
                        new CluesStructure { Name = "Minus", Source = "Minus_icon.png" }
                    },
                    ImageUrl="Mara.jpg",
                    Title="Mara"
                },
                new Ghost()
                {
                    Clues=new List<CluesStructure>
                    {
                        new CluesStructure { Name="Minus", Source="Minus_icon.png"},
                        new CluesStructure { Name = "Radio", Source = "Radio_icon.png" },
                        new CluesStructure { Name = "Book", Source = "Book_icon.png" }
                    },
                    ImageUrl="Banshi.jpg",
                    Title="Banshi"
                },
                new Ghost()
                {
                    Clues=new List<CluesStructure>
                    {
                        new CluesStructure { Name="Book", Source="Book_icon.png"},
                        new CluesStructure { Name = "Minus", Source = "Minus_icon.png" },
                        new CluesStructure { Name = "Radio", Source = "Radio_icon.png" }
                    },
                    ImageUrl="Polter.jpg",
                    Title="Polter"
                },
            };
            CreatePageContent();

        }

        private void CreatePageContent()
        {
            List<PancakeView> pancakeViews = new List<PancakeView>();
            for (int i = 0; i < ghosts.Count; i++)
            {

                pancakeViews.Add(new PancakeView()
                {
                    Padding = new Thickness(10),
                    BackgroundColor = Color.White,
                    Shadow = new DropShadow()
                    {
                        Color = Color.Black,
                        Offset = new Point(0, 3)
                    },
                    Border = new Border()
                    {
                        Color = Color.Default,
                        Thickness = 1
                    },
                    CornerRadius = new CornerRadius(15),
                });
                var stackLayout = new StackLayout();
                var imageP = new Image
                {
                    Source = ghosts[i].ImageUrl
                };
                var label = new Label
                {
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Text = ghosts[i].Title,
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };
                var iconStack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };
                for (int j = 0; j < ghosts[i].Clues.Count; j++)
                {
                    PancakeView pancakes = new PancakeView
                    {
                        HeightRequest = 30,
                        WidthRequest = 30,
                        CornerRadius = 15,
                        BackgroundColor = Color.White,
                        Padding = 5,
                        Shadow = new DropShadow
                        {
                            Color = Color.Black,
                            Offset = new Point(0, 1)
                        }
                    };

                    var icon = new Image
                    {
                        Source = ghosts[i].Clues[j].Source,
                        Aspect = Aspect.AspectFit
                    };

                    pancakes.Content = icon;
                    iconStack.Children.Add(pancakes);
                }
                stackLayout.Children.Add(imageP);
                stackLayout.Children.Add(label);
                stackLayout.Children.Add(iconStack);
                pancakeViews[i].Content = stackLayout;
                MainGrid.Children.Add(pancakeViews[i]);
                Grid.SetRow(pancakeViews[i], i / 2);
                Grid.SetColumn(pancakeViews[i], i % 2);
                MainGrid.RowDefinitions.Add(new RowDefinition { Height = 260 });
            }
        }
    }
}