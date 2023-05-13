using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassLib
{
    public class LotteryProgram
    {
        public LotteryPeriod p = new LotteryPeriod(40_000_000);//Start at $40M
        public LotteryVendor lv;
        public List<Task> VendorSellingTasks { get; private set; } = new();

        public LotteryProgram()
        {
            lv = new LotteryVendor(p); //starting with one Vendor 
            p.SalesState = TicketSales.CLOSED;
            
        }
        public bool ClosePeriodSales()
        {
            if (p.SalesState == TicketSales.OK)
            {
                p.SalesState = TicketSales.CLOSED;
                return true;
            }
            else
                return false;
        }
        public bool EnableSalesPeriod()
        {
            if (p.SalesState == TicketSales.CLOSED)
            {
                p = new LotteryPeriod(40_000_000);
                lv=new LotteryVendor(p);

                //TODO - how will you tell the vendor/clients that VendorResults are not availble?
                WeakReferenceMessenger.Default.Send(new VendorResultsNotAvailable());
                p.SalesState = TicketSales.OK;
                return true;
            }
            else
                return false;
        }
        
    }
}
