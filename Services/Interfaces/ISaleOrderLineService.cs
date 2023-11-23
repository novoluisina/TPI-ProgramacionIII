﻿using TPI_ProgramacionIII.Data.Entities;

namespace TPI_ProgramacionIII.Services.Interfaces
{
    public interface ISaleOrderLineService
    {
        List<SaleOrderLine> GetAllBySaleOrder(int saleOrderId);
        SaleOrderLine? GetOne(int Id);
        SaleOrderLine CreateSaleOrderLine(SaleOrderLine saleOrderLine);
        SaleOrderLine UpdateSaleOrderLine(SaleOrderLine sol);
        void DeleteSaleOrderLine(int id);
        List<SaleOrderLine> GetAllByProduct(int productId);
    }
}
