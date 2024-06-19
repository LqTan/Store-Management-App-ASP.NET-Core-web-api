using AutoMapper;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;

namespace EcommerceApp.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {            
            CreateMap<Brand, BrandModel>().ReverseMap();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Discount, DiscountModel>().ReverseMap();
            CreateMap<Inventory, InventoryModel>().ReverseMap();
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailModel>().ReverseMap();
            CreateMap<Payment, PaymentModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Return, ReturnModel>().ReverseMap();
            CreateMap<Review, ReviewModel>().ReverseMap();
            CreateMap<Shipment, ShipmentModel>().ReverseMap();
            CreateMap<ShipmentCarrier, ShipmentCarrierModel>().ReverseMap();
            CreateMap<Warehouse, WarehouseModel>().ReverseMap();
        }
    }
}
