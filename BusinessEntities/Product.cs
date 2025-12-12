using System;

namespace BusinessEntities
{
    public class Product : IdNameObject
    {
        private string _description;
        private decimal _price;
        private long _stock;
        private DateTime? _availableFrom;
        private DateTime? _availableTo;
        private ProductGraphics[] _graphics = Array.Empty<ProductGraphics>();
        private ProductMetaData[] _metaData = Array.Empty<ProductMetaData>();

        public string Description
        {
            get => _description;
            private set => _description = value;
        }

        public decimal Price
        {
            get => _price;
            private set => _price = value;
        }

        public long Stock
        {
            get => _stock;
            private set => _stock = value;
        }

        public DateTime? AvailableFrom
        {
            get => _availableFrom;
            private set => _availableFrom = value;
        }
        public DateTime? AvailableTo
        {
            get => _availableTo;
            private set => _availableTo = value;
        }

        public ProductGraphics[] Graphics {
            get => _graphics;
            private set => _graphics = value;
        }

        public ProductMetaData[] MetaData
        {
            get => _metaData;
            private set => _metaData = value;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }

        public void SetPrice(decimal price)
        {
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException("Price cannot be negative.");
            }
            _price = price;
        }

        public void SetStock(long stock)
        {
            if (stock < 0)
            {
                throw new ArgumentOutOfRangeException("Stock cannot be negative.");
            }
            _stock = stock;
        }

        public void SetAvailability(DateTime? availableFrom, DateTime? availableTo)
        {
            if (availableFrom.HasValue && availableTo.HasValue && availableFrom > availableTo)
            {
                throw new ArgumentException("AvailableFrom cannot be later than AvailableTo.");
            }
            _availableFrom = availableFrom;
            _availableTo = availableTo;
        }

        public void SetGraphics(ProductGraphics[] graphics)
        {
            _graphics = graphics ?? Array.Empty<ProductGraphics>();
        }

        public void SetMetaData(ProductMetaData[] metaData)
        {
            _metaData = metaData ?? Array.Empty<ProductMetaData>();
        }

        public DateTime CreatedDate { get; } = DateTime.Now;

        public DateTime UpdatedDate { get; } = DateTime.Now;

        //populated using jwt token
        public string CreatedUser { get; set; }

    }

    public class ProductGraphics
    {
        public ProductGraphics(string imageUrl, string thumbnailUrl, string title)
        {
            ImageUrl = imageUrl;
            ThumbnailUrl = thumbnailUrl;
            Title = title;
        }

        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
    }

    public class ProductMetaData
    {

        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
    }
}
