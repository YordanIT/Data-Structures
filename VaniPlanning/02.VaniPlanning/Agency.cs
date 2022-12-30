namespace _02.VaniPlanning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Agency : IAgency
    {
        private Dictionary<string, Invoice> invoices = new Dictionary<string, Invoice>();

        public void Create(Invoice invoice)
        {
            if (Contains(invoice.SerialNumber))
            {
                throw new ArgumentException();    
            }

            invoices.Add(invoice.SerialNumber, invoice);
        }

        public void ThrowInvoice(string number)
        {
            if (!Contains(number))
            {
                throw new ArgumentException();
            }

            invoices.Remove(number);
        }

        public void ThrowPayed()
        {
            foreach (var invoice in invoices.Values)
            {
                if (invoice.Subtotal == 0)
                {
                    invoices.Remove(invoice.SerialNumber);
                }
            }
        }

        public int Count()
        {
            return invoices.Count;
        }

        public bool Contains(string number)
        {
            return invoices.ContainsKey(number);
        }

        public void PayInvoice(DateTime due)
        {
            if (!invoices.Values.Any(x => x.DueDate == due))
            {
                throw new ArgumentException();
            }

            foreach (var invoice in invoices.Values)
            {
                if (invoice.DueDate == due)
                {
                    invoice.Subtotal = 0;
                }
            }
        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
        {
            var result = invoices.Values.Where(x => x.IssueDate >= start && x.IssueDate <= end)
                .OrderBy(x => x.IssueDate)
                .ThenBy(x => x.DueDate)
                .ToArray();

            return result;
        }

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            if (!invoices.Keys.Any(x => x.Contains(serialNumber)))
            {
                throw new ArgumentException();
            }

            return invoices.Values.Where(x => x.SerialNumber.Contains(serialNumber))
                .OrderByDescending(x => x.SerialNumber);
        }

        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {
            var serialNumbers = invoices.Values.Where(x => x.DueDate > start && x.DueDate < end);
 
            if (!serialNumbers.Any())
            {
                throw new ArgumentException();
            }

            foreach (string invoiceSR in invoices.Keys)
            {
                if (serialNumbers.Any(x => x.SerialNumber == invoiceSR))
                {
                    invoices.Remove(invoiceSR);
                }
            }

            return serialNumbers;
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
        {
            return invoices.Values.Where(x => x.Department == department)
                .OrderByDescending(x => x.Subtotal)
                .ThenBy(x => x.IssueDate);
        }

        public IEnumerable<Invoice> GetAllByCompany(string company)
        {
            return invoices.Values.Where(x => x.CompanyName == company)
                .OrderByDescending(x => x.SerialNumber);
        }

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            if (!invoices.Values.Any(x => x.DueDate == dueDate))
            {
                throw new ArgumentException();
            }

            foreach (var invoice in invoices.Values)
            {
                if (invoice.DueDate == dueDate)
                {
                    invoice.DueDate = invoice.DueDate.AddDays(days);
                }
            }
        }
    }
}
