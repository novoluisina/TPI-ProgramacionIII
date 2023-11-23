﻿using Microsoft.EntityFrameworkCore;
using TPI_ProgramacionIII.Data.Entities;
using TPI_ProgramacionIII.DBContexts;
using TPI_ProgramacionIII.Services.Interfaces;

namespace TPI_ProgramacionIII.Services.Implementations
{
    public class SaleOrderLineService : ISaleOrderLineService
    {
        private readonly ECommerceContext _context;

        public SaleOrderLineService(ECommerceContext context)
        {
            _context = context;
        }

        public List<SaleOrderLine> GetAllBySaleOrder(int saleOrderId) //todas las sol por cada so
        {
            return _context.SaleOrderLines
                .Include(sol => sol.Product)
                .Include(sol => sol.SaleOrder)
                .ThenInclude(so => so.Client)
                .Where(sol => sol.SaleOrderId == saleOrderId)
                .ToList();
        }

        public List<SaleOrderLine> GetAllByProduct(int productId) //todas las sol por cada producto
        {
            return _context.SaleOrderLines
                .Include(sol => sol.Product)
                .Where(sol => sol.ProductId == productId)
                .Include(sol => sol.SaleOrder)
                .ThenInclude(so => so.Client)
                .ToList();
        }

        public SaleOrderLine? GetOne(int Id)
        {
            return _context.SaleOrderLines
                .Include(sol => sol.Product)
                .Include(sol=> sol.SaleOrder)
                .ThenInclude(so => so.Client)
                .SingleOrDefault(x => x.Id == Id);
        }

        public SaleOrderLine CreateSaleOrderLine(SaleOrderLine saleOrderLine)
        {
            _context.Add(saleOrderLine);
            _context.SaveChanges();
            return saleOrderLine;
        }

        public SaleOrderLine UpdateSaleOrderLine(SaleOrderLine sol)
        {
            _context.Update(sol);
            _context.SaveChanges();
            return sol;
        }

        public void DeleteSaleOrderLine(int id)
        {
            var solToDelete = _context.SaleOrderLines.SingleOrDefault(p => p.Id == id);

            if (solToDelete != null)
            {
                _context.SaleOrderLines.Remove(solToDelete);
                _context.SaveChanges();
            }
        }
    }
}


