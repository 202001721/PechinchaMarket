using Microsoft.AspNetCore.Mvc;
using PechinchaMarket.Areas.Identity.Data;
using PechinchaMarket.Models;
using Quartz;
using System.Diagnostics;

namespace PechinchaMarket
{
    public class ScheduledJobs : IJob
    {
        private readonly DBPechinchaMarketContext _context;

        public ScheduledJobs(DBPechinchaMarketContext context)
        {
            _context = context;
        }


        [HttpPost]
        public Task Execute(IJobExecutionContext context)
        {
            Debug.WriteLine("This is a scheduled update to the database, it started, let's see if it does something");
            foreach (ProdutoLoja prodLoja in _context.ProdutoLoja)
            {
                if(prodLoja != null && prodLoja.EndDiscount <= DateTime.Now)
                {
                    prodLoja.EndDiscount = null;
                    prodLoja.StartDiscount = null;
                    prodLoja.Discount = null;
                    _context.SaveChanges();

                    Debug.WriteLine("If you're seeing this, it worked!");
                }
            }

            return Task.FromResult(true);
        }
    }
}
