using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamarinShoppingCart.Model;

namespace XamarinShoppingCart.ViewModel
{
	public class ProductCatalogViewModel : ViewModelBase
	{
        public List<Product> ProductList { get; set; }
        public List<ProductCatalog> ProductCatalogs { get; set; }
        public static string ProductListFile = "TempProductList.txt";
        public ObservableCollectionFast<CartItem> CartItems { get; set; }
        public static string CartListFile = "TempCartList.txt";
        private HttpClient _client;
        private string Currency = "RM";

        public ProductCatalogViewModel()
        {
            ProductCatalogs = new List<ProductCatalog>();
            CartItems = new ObservableCollectionFast<CartItem>();

            //create http client
            _client = CreateHTTPClient();

            //test server connection
            var serverOnline = false;
            Task.Run(async () => { serverOnline = await TestServerConnection(_client); }).Wait();
            serverOnline = false;
            //retrieve data for product catalog
            if (serverOnline)
            {
                Task.Run(async () => { ProductList = await GetProductAsync(_client); }).Wait();
                Task.Run(async () => { await SaveProductDataToFile(ProductList, ProductListFile); }).Wait();
            }
            else
            {
                Task.Run(async () => { ProductList = await ReadProductDataFromFile(ProductListFile); }).Wait();       
            }

            foreach (Product product in ProductList)
            {
                ProductCatalogs.Add(
                    new ProductCatalog { Product = product  , Qty = 1, PriceWithCurrency = Currency + " " + product.Regular_price});
            }

            //FillCatalog();

            //Retrieve cart data
            Task.Run(async () => { CartItems = await ReadCartDataFromFile(CartListFile); }).Wait();   
        }

        //      Product selectedProduct;

        //      public Product SelectedProduct
        //{
        //          get { return selectedProduct; }
        //          set
        //          {
        //              if (selectedProduct != value)
        //              {
        //                  selectedProduct = value;
        //                  OnPropertyChanged();
        //              }
        //          }
        //      }

        private HttpClient CreateHTTPClient()
        {
            HttpClient client = new HttpClient();
            var webAPI_username = "ck_2682b35c4d9a8b6b6effac126ac552e0bfb315a0";
            var webAPI_password = "cs_cab8c9a729dfb49c50ce801a9ea41b577c00ad71";
            var authenticationString = $"{webAPI_username}:{webAPI_password}";
            var byteArray = Encoding.ASCII.GetBytes(authenticationString);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return client;
        }

        private async Task<bool> TestServerConnection(HttpClient client)
        {
            var connectionSuccessful = false;
            var uri = new Uri("https://mangomart-autocount.myboostorder.com/wp-json/wc/v1/products");

            try
            {
                var responseMsg = await _client.GetAsync(uri);

                if (responseMsg.IsSuccessStatusCode)
                {
                    var content = await responseMsg.Content.ReadAsStringAsync();
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    List<Product> results = JsonConvert.DeserializeObject<List<Product>>(content, settings);
                    //results = new List<Product>();
                    if (results.Any())
                    {
                        connectionSuccessful = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
                return connectionSuccessful;
            }

            return connectionSuccessful;
        }

        private async Task<List<Product>> GetProductAsync(HttpClient client)
        { 
            var uri = new Uri("https://mangomart-autocount.myboostorder.com/wp-json/wc/v1/products?page=");
            var Continue_Request = true;
            var CurrentPage = 1;
            var TotalPages = "";
            //var TotalProduct = "";
            List<Product> ListProduct = new List<Product>();

            do
            {
                try
                {
                    var responseMsg = await client.GetAsync(uri + CurrentPage.ToString());

                    if (responseMsg.IsSuccessStatusCode)
                    {
                        TotalPages = responseMsg.Headers.TryGetValues("X-WP-TotalPages", out var totalPages) ? totalPages.FirstOrDefault() : null;
                        //TotalProduct = responseMsg.Headers.TryGetValues("X-WP-Total", out var totalProduct) ? totalProduct.FirstOrDefault() : null;
                        var content = await responseMsg.Content.ReadAsStringAsync();
                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };

                        List<Product> results = JsonConvert.DeserializeObject<List<Product>>(content, settings);
                        ListProduct.AddRange(results);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("\tERROR {0}", ex.Message);
                }

                //check for more pages
                //empty string can be fail request which should abort the loop as well
                if (String.IsNullOrEmpty(TotalPages))
                {
                    Continue_Request = false;
                }
                else
                {
                    if (CurrentPage < int.Parse(TotalPages))
                    {
                        CurrentPage += 1;
                    }
                    else
                    {
                        Continue_Request = false;
                    }
                }
            } while (Continue_Request);

            ProductList = ListProduct;
            return ListProduct;
        }

        public static async Task SaveProductDataToFile(List<Product> Products, string ProductListFile)
        {
            var json = JsonConvert.SerializeObject(Products, Formatting.Indented);

            try
            {
                var StoredFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ProductListFile);
                using (var writer = File.CreateText(StoredFile))
                {
                    await writer.WriteLineAsync(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
        }

        public static async Task<List<Product>> ReadProductDataFromFile(string ProductListFile)
        {
            List<Product> results = new List<Product>();
            try
            {
                var StoredFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ProductListFile);

                if (StoredFile == null || !File.Exists(StoredFile))
                {
                    return results;
                }

                string FileData = string.Empty;
                using (var reader = new StreamReader(StoredFile, true))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        FileData += line;
                    }
                }

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                results = JsonConvert.DeserializeObject<List<Product>>(FileData, settings);

                return results;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
                return results;
            }
        }

        public static async Task SaveCartDataToFile(ObservableCollectionFast<CartItem> CartItems, string CartListFile)
        {
            var json = JsonConvert.SerializeObject(CartItems, Formatting.Indented);

            try
            {
                var StoredFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), CartListFile);
                using (var writer = File.CreateText(StoredFile))
                {
                    await writer.WriteLineAsync(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }
        }

        public static async Task<ObservableCollectionFast<CartItem>> ReadCartDataFromFile(string CartListFile)
        {
            ObservableCollectionFast<CartItem> results = new ObservableCollectionFast<CartItem>();

            try
            {
                var StoredFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), CartListFile);

                if (StoredFile == null || !File.Exists(StoredFile))
                {
                    return results;
                }

                string FileData = string.Empty;
                using (var reader = new StreamReader(StoredFile, true))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        FileData += line;
                    }
                }

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                results = JsonConvert.DeserializeObject<ObservableCollectionFast<CartItem>>(FileData, settings);

                return results;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
                return results;
            }
        }

        void FillCatalog()
        {
            ProductCatalogs = new List<ProductCatalog>();

            ProductCatalogs.Add(new ProductCatalog
            {
                Product = new Product()
                {
                    Id = "1001",
                    Name = "Test Item",
                    Regular_price = 1.00F,
                    Sku = "TI1102",
                    Type = "Unknown",
                    Images = new List<ProductImage>
                    {
                        new ProductImage()
                        {
                            Src = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                        }
                    }
                },
                Qty = 1
            });

            ProductCatalogs.Add(new ProductCatalog
            {
                Product = new Product()
                {
                    Id = "1002",
                    Name = "Test Item2",
                    Regular_price = 2.00F,
                    Sku = "TI1103",
                    Type = "Unknown",
                    Images = new List<ProductImage>
                    {
                        new ProductImage()
                        {
                            Src = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                        }
                    }
                },
                Qty = 1
            });

            ProductCatalogs.Add(new ProductCatalog
            {
                Product = new Product()
                {
                    Id = "1003",
                    Name = "Test Item3",
                    Regular_price = 4.00F,
                    Sku = "TI1122",
                    Type = "Unknown",
                    Images = new List<ProductImage>
                    {
                        new ProductImage()
                        {
                            Src = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
                        }
                    }
                },
                Qty = 1
            });
        }

    }
}