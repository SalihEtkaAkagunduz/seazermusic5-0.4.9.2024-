using Microsoft.Maui.Controls;
using seazermusic5.Views;

namespace seazermusic5
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
#if WINDOWS
            Build();
#elif ANDROID
            BuildB();
#endif
            this.Navigated += OnNavigated;
        }
        private void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            // Tüm sekmelerin simgelerini varsayılan haline döndür
            if (CurrentItem is TabBar currentTabBar)
            {
                foreach (var item in currentTabBar.Items)
                {
                    if (item is ShellSection section)
                    {
                        section.Icon = GetDefaultIcon(section.Title);
                    }
                }

                // Geçerli sekmenin simgesini değiştir
                if (currentTabBar.CurrentItem is ShellSection currentSection)
                {
                    currentSection.Icon = GetSelectedIcon(currentSection.Title);
                }
            }
        }


        private string GetDefaultIcon(string title)
        {
            return title switch
            {
                "Home" => "bar1.png",
                "Gözat" => "bar2.png",
                "Ara" => "a20.png",
                "Arşiv" => "bar4.png",
                "Premium" => "ass2.png",
                _ => "default.png"
            };
        }

        private string GetSelectedIcon(string title)
        {
            return title switch
            {
                "Home" => "bar1b.png",
                "Gözat" => "bar2b.png",
                "Ara" => "bar3b.png",
                "Arşiv" => "bar4b.png",
                "Premium" => "ass.png",
                _ => "default.png"
            };
        }
        private void asas_Clicked(object sender, EventArgs e)
        {
            MenuItem aasd = (MenuItem)sender;
            if (Shell.Current.FlyoutWidth == 65)
            {
                Shell.Current.FlyoutWidth = 190;
                aasd.IconImageSource = "asdddd.png";
            }
            else
            {
                Shell.Current.FlyoutWidth = 65;
                aasd.IconImageSource = "asddd.png";
            }
        }
        private void BuildB()
        {


            var tabBar = new TabBar();
            
            // Tabları oluştur
            var homeTab = new ShellContent
            {
                
                Title = "Home",
                Icon = "home.png",
                Content = new MainMenu()
            };

            var searchTab = new ShellContent
            {
                Title = "Gözat",
                Icon = "grid.png",
                Content = new Gözat()
            };

            var settingsTab = new ShellContent
            {
                Title = "Ara",
                Icon = "a20.png",
                Content = new ogebul(),
                
            };
 var settingsTab2 = new ShellContent
            {
                Title = "Arşiv",
                Icon = "fff.png",
                Content = new arsiv()
            }; var settingsTab3 = new ShellContent
            {
                Title = "Premium",
                Icon = "diamond.png",
                Content = new Premium() 
                
            };
            // Tabları tabBar'a ekle
            tabBar.Items.Add(homeTab);
            tabBar.Items.Add(searchTab);
            tabBar.Items.Add(settingsTab);
            tabBar.Items.Add(settingsTab2);
            tabBar.Items.Add(settingsTab3);
            // Shell'e tabBar'ı ekle
            Items.Add(tabBar);
            
            Shell.SetTabBarTitleColor(this, Color.FromHex("#3572EF")); // Tab başlıkları için renk

           
        }

        
        private void BuildA()
        {
          
        

           

             

          

            
 
            this.Items.Add(new FlyoutItem
            {
                Title = "Home",
                Icon = "Home.png",
               
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Route = "Domedsfgstidc",
                Items =
                {new ShellContent
            {
                Title = "Home",
                Icon = "home.png",
                ContentTemplate = new DataTemplate(typeof(MainMenu)),
                Route = "Cavsdfgbnts"
            },new ShellContent
            {
                Title = "Göz At",
                Icon = "grid.png",
                ContentTemplate = new DataTemplate(typeof(Gözat)),
                Route = "Cavsdfsbnts"
            },new ShellContent
            {
                Title = "Ara",
                Icon = "a20.png",   
                ContentTemplate = new DataTemplate(typeof(ogebul)),
                Route = "Dogdsvbns"
            },new ShellContent
            {
                Title = "Arşiv",
                Icon = "fff.png",
                ContentTemplate = new DataTemplate(typeof(arsiv)),
                Route = "Dogsfgsdvbns"
            },
                    new ShellContent
            {
                Title = "Premium",
                Icon = "diamond.png",
                
                ContentTemplate = new DataTemplate(typeof(Premium)),
                Route = "Dogsdgsdvbns"
            },
                }
            });
           

            // Geri tuşunu ekleyin
            this.Items.Add(new MenuItem
            {
                Text = "Geri",
                IconImageSource = "asdddd.png",
                Command = new Command(async () =>
                {
                    System.Diagnostics.Debug.WriteLine("Geri tuşuna basıldı");
                    await Shell.Current.GoToAsync("..");
                })
            });
        
    }


        private void Build()
        {
            this.FlyoutBehavior = FlyoutBehavior.Flyout;
            this.FlyoutWidth = 190;
            this.IconImageSource = "logoooo.png";
            this.Title = "Seazer Music";

            var demoFlyoutItem = new FlyoutItem
            {
                Title = "Demo(test bölümü)",
                Icon = "p.png",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem
            };

            demoFlyoutItem.Items.Add(new ShellContent
            {
                Title = "Kayısdatlı Şarkılar",
                Icon = "f.png",
                ContentTemplate = new DataTemplate(typeof(kayıtlısarki)),
                Route = "Cavbnts"
            });

            demoFlyoutItem.Items.Add(new ShellContent
            {
                Title = "Kasyıtlı Listeler",
                Icon = "uu.png",
                ContentTemplate = new DataTemplate(typeof(sarkibul)),
                Route = "Dogvbns"
            });

            demoFlyoutItem.Items.Add(new ShellContent
            {
                Title = "Kayıtadlı Listeler",
                Icon = "uu.png",
                ContentTemplate = new DataTemplate(typeof(listeler)),
                Route = "Dogvbns"
            });

            demoFlyoutItem.Items.Add(new ShellContent
            {
                Title = "podcaasdst",
                Icon = "uu.png",
                ContentTemplate = new DataTemplate(typeof(podcastlist)),
                Route = "Dogvbns"
            });

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                this.Items.Add(demoFlyoutItem);
            }

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                var demoFlyoutItemWinUI = new FlyoutItem
                {
                    Title = "Demo(test bölümü)",
                    Icon = "p.png",
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem
                };

                demoFlyoutItemWinUI.Items.Add(new ShellContent
                {
                    Title = "Kayıtlı Şarkılar",
                    Icon = "f.png",
                    ContentTemplate = new DataTemplate(typeof(kayıtlısarki)),
                    Route = "Cavbnts"
                });

                demoFlyoutItemWinUI.Items.Add(new ShellContent
                {
                    Title = "Kayıtlı Listeler",
                    Icon = "uu.png",
                    ContentTemplate = new DataTemplate(typeof(sarkibul)),
                    Route = "Dogvbns"
                });

                demoFlyoutItemWinUI.Items.Add(new ShellContent
                {
                    Title = "Kayıtlı Listeler",
                    Icon = "uu.png",
                    ContentTemplate = new DataTemplate(typeof(listeler)),
                    Route = "Dogvbns"
                });

                demoFlyoutItemWinUI.Items.Add(new ShellContent
                {
                    Title = "podcast",
                    Icon = "uu.png",
                    ContentTemplate = new DataTemplate(typeof(podcastlist)),
                    Route = "Dogvbns"
                });

                this.Items.Add(demoFlyoutItemWinUI);
            }

            this.Items.Add(new FlyoutItem
            {
                Title = "Şimdi Dinle",
                Icon = "v.png",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Route = "Domesdddddtic",
                Items =
                {
                    new ShellContent
                    {
                        Title = "Şarkılar",
                        Icon = "Simge/a.png",
                        ContentTemplate = new DataTemplate(typeof(MainPage)),
                        Route = "Cats"
                    }
                }
            });

            this.Items.Add(new FlyoutItem
            {
                Title = "Gözat",
                Icon = "o.png",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Route = "Domesdddtic",
                Items =
                {
                    new ShellContent
                    {
                        Title = "Şarkılar",
                        Icon = "b.png",
                        ContentTemplate = new DataTemplate(typeof(Page3)),
                        Route = "Caghdfts"
                    }
                }
            });

            this.Items.Add(new FlyoutItem
            {
                Title = "Podcast",
                Icon = "podcast_image.png",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Route = "Domestidc",
                Items =
                {
                    new ShellContent
                    {
                        Title = "Şarkılar",
                        Icon = "cc.png",
                        ContentTemplate = new DataTemplate(typeof(podcastlist)),
                        Route = "Cats"
                    }
                }
            });

            this.Items.Add(new FlyoutItem
            {
                Title = "Arşiv",
                Icon = "fff.png",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Route = "Domesddticvv",
                Items =
                {
                    new ShellContent
                    {
                        Title = "Listeler",
                        Icon = "aaa.png",
                        ContentTemplate = new DataTemplate(typeof(listeler)),
                        Route = "Cavbnts"
                    },
                    new ShellContent
                    {
                        Title = "Şarkılar",
                        Icon = "a9.png",
                        ContentTemplate = new DataTemplate(typeof(kayıtlısarki)),
                        Route = "Dodgvdbns"
                    },
                    new ShellContent
                    {
                        Title = "Sanatçılar",
                        Icon = "a14.png",
                        ContentTemplate = new DataTemplate(typeof(kayıtlısarki)),
                        Route = "Cavdbnts"
                    },
                    new ShellContent
                    {
                        Title = "Albümler",
                        Icon = "a2.png",
                        ContentTemplate = new DataTemplate(typeof(sarkibul)),
                        Route = "Dodgvbddns"
                    },
                    new ShellContent
                    {
                        Title = "Size Özel",
                        Icon = "a6.png",
                        ContentTemplate = new DataTemplate(typeof(sarkibul)),
                        Route = "Dogvbndds"
                    },
                    new ShellContent
                    {
                        Title = "İndirilenler",
                        Icon = "a16.png",
                        ContentTemplate = new DataTemplate(typeof(indirilenler)),
                        Route = "Dodddgvbns"
                    },
                    new ShellContent
                    {
                        Title = "Podcastlar",
                        Icon = "a15.png",
                        ContentTemplate = new DataTemplate(typeof(podcastlist)),
                        Route = "Dodgvbns"
                    }
                }
            });

            this.Items.Add(new FlyoutItem
            {
                Title = "Listeler",
                Icon = "e.png",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Route = "Domesdvvdtic",
                Items =
                {
                    new ShellContent
                    {
                        Title = "Şarkılar",
                        Icon = "icon_about.png",
                        ContentTemplate = new DataTemplate(typeof(listeler)),
                        Route = "Casfdsfts"
                    }
                }
            });

            this.Items.Add(new FlyoutItem
            {
                Title = "Ara",
                Icon = "a20.png",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Route = "Domesdvvdtic",
                Items =
                {
                    new ShellContent
                    {
                        Title = "Liste Bul",
                        Icon = "icon_about.png",
                        ContentTemplate = new DataTemplate(typeof(listebul)),
                        Route = "Ca465shdfhts"
                    },
                    new ShellContent
                    {
                        Title = "Şarkı Bul",
                        Icon = "icon_about.png",
                        ContentTemplate = new DataTemplate(typeof(sarkibul)),
                        Route = "Cashdfhts"
                    },
                    new ShellContent
                    {
                        Title = "Podcast Bul",
                        Icon = "icon_about.png",
                        ContentTemplate = new DataTemplate(typeof(podcastbul)),
                        Route = "Cadfhshts"
                    },
                    new ShellContent
                    {
                        Title = "Podcast Bul",
                        Icon = "icon_about.png",
                        ContentTemplate = new DataTemplate(typeof(kanalbul)),
                        Route = "Cadfhsgfdjhts"
                    },
                    new ShellContent
                    {
                        Title = "Podcast Bul",
                        Icon = "icon_about.png",
                        ContentTemplate = new DataTemplate(typeof(ogebul)),
                        Route = "fdhdf"
                    }
                }
            });

            this.Items.Add(new FlyoutItem
            {
                Title = "Premium",
                Icon = "y.png",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Route = "Domesddvvtic",
                Items =
                {
                    new ShellContent
                    {
                        Title = "Şarkılar",
                        Icon = "a.png",
                        ContentTemplate = new DataTemplate(typeof(Premium)),
                        Route = "Cats"
                    }
                }
            });

            this.Items.Add(new FlyoutItem
            {
                Title = "Hesap Ayarları",
                Icon = "kk.png",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Route = "Domesddvvtic",
                Items =
                {
                    new ShellContent
                    {
                        Title = "Şarkılar",
                        Icon = "a.png",
                        ContentTemplate = new DataTemplate(typeof(Premium)),
                        Route = "Cats"
                    }
                }
            });

            this.Items.Add(new FlyoutItem
            {
                Title = "Hesap Ayarları",
                Icon = "kk.png",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Route = "Domevvvsddtic",
                Items =
                {
                    new ShellContent
                    {
                        Title = "Şarkılar",
                        Icon = "cc.png",
                        ContentTemplate = new DataTemplate(typeof(User)),
                        Route = "Cats"
                    }
                }
            });

            this.Items.Add(new FlyoutItem
            {
                Title = "Ayarlar",
                Icon = "ll.png",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsSingleItem,
                Route = "Domevvvvvsddtic",
                Items =
                {
                    new ShellContent
                    {
                        Title = "Şarkılar",
                        Icon = "icon_about.png",
                        ContentTemplate = new DataTemplate(typeof(Ayarlar)),
                        Route = "Cats"
                    }
                }
            });

            this.Items.Add(new MenuItem
            {
                Text = "Geri",
                IconImageSource = "asdddd.png",
                Command = new Command(async () =>
                {
                    System.Diagnostics.Debug.WriteLine("Geri tuşuna basıldı");
                    await Shell.Current.GoToAsync("..");
                })
            });
        }
    }
}
