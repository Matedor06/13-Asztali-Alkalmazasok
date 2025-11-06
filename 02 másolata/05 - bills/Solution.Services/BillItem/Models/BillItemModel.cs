using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Services.BillItem.Models
{
    public partial class BillItemModel: ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int unitPrice;

        [ObservableProperty]
        private int quantity;

        public BillItemModel()
        {
        }

        public BillItemModel(int id, string name, int unitPrice, int quantity)
        {
            this.Id = id;
            this.Name = name;
            this.UnitPrice = unitPrice;
            this.Quantity = quantity;
        }

        public BillItemModel(BillItemEntity entity)
        {
            if(entity is null)
            {
                return;
            }
            Id = entity.Id;
            Name = entity.Name;
            UnitPrice = entity.UnitPrice;
            Quantity = entity.Quantity;
        }

        public BillItemEntity ToEntity()
        {
            return new BillItemEntity
            {
                Id = this.Id,
                Name = this.Name,
                UnitPrice = this.UnitPrice,
                Quantity = this.Quantity
            };
        }

        public void ToEntity(BillItemEntity entity)
        {
            if (entity is null)
            {
                return;
            }
            entity.Id = this.Id;
            entity.Name = this.Name;
            entity.UnitPrice = this.UnitPrice;
            entity.Quantity = this.Quantity;
        }

        public override bool Equals(object? obj)
        {
            return obj is BillItemModel model &&
                   Id == model.Id &&
                   Name == model.Name &&
                   UnitPrice == model.UnitPrice &&
                   Quantity == model.Quantity;
        }
    }
}
