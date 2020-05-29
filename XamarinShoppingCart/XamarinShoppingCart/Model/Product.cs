using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XamarinShoppingCart.Model
{
    public class Product
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("regular_price")]
        public float Regular_price { get; set; }

        [JsonProperty("images")]
        public List<ProductImage> Images { get; set; }

        //[JsonProperty("attributes")]
        //public List<ProductAttribute> Attributes
        //{
        //    get;
        //    set;
        //    //get
        //    //{
        //    //    return Attributes;
        //    //}
        //    //set
        //    //{
        //    //    if (Attributes != value && null != value)
        //    //    {
        //    //        Attributes = value;
        //    //    }
        //    //    else
        //    //    {
        //    //        Attributes = new List<ProductAttribute>();
        //    //        Attributes.Add("");
        //    //    }
        //    //}
        //}

    }

    public class ProductImage
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("alt")]
        public string Alt { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("src_small")]
        public string Src_small { get; set; }

        [JsonProperty("src_medium")]
        public string Src_medium { get; set; }

        [JsonProperty("src_large")]
        public string Src_large { get; set; }
    }

    //public class ProductAttribute
    //{
    //    [JsonProperty("options")]
    //    public List<string> Options { get; set; }

    //}

    //public class ProductAttribute
    //{
    //    [JsonProperty("options")]
    //    public string[] Options
    //    {
    //        //get;
    //        //set;
    //        get
    //        {
    //            return Options;
    //        }
    //        set
    //        {
    //            if (Options != value && null != value)
    //            {
    //                Options = value;
    //            }
    //            else
    //            {
    //                Options = new string[1]; 
    //                Options[0] = "Qty-Def";
    //            }
    //        }
    //    }
    //}

    //public class ProductAttribute
    //{
    //    [JsonProperty("options")]
    //    public List<string> Options
    //    {
    //        //get;
    //        //set;
    //        get
    //        {
    //            return Options;
    //        }
    //        set
    //        {
    //            if (Options != value && null != value)
    //            {
    //                Options = value;
    //            }
    //            else
    //            {
    //                Options = new List<string>();
    //                Options.Add("Qty-Def");
    //            }
    //        }
    //    }


    //    //public override string ToString()
    //    //{
    //    //    return Options.ToString();
    //    //}
    //    //public IList<Monkey> Monkeys { get { return MonkeyData.Monkeys; } }
    //}

    public class ProductAttributeOption
    {
        public string Option { get; set; }
    }
}
