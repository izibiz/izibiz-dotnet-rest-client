using Izibiz.Adapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Izibiz
{
    public class IzibizClient
    {
        private readonly AuthAdapter _authAdapter;
        private readonly EArchiveInvoiceAdapter _eArchiveInvoiceAdapter;
        private readonly EInvoiceAdapter _eInvoiceAdapter;
        private readonly EDespatchAdapter _eDespatchAdapter;
        private readonly EDespatchReceiptAdapter _eDespatchReceiptAdapter;
        private readonly CreditNoteAdapter _creditNoteAdapter;
        private readonly ESmmAdapter _eSmmAdapter;
        private readonly EExchangeAdapter _eCurrencyAdapter;
        private readonly ECheckAdapter _eTicketAdapter;

        //public IzibizClient(string username, string password, string baseUrl)
        //{
        //    var requestOptions = new RequestOptions
        //    {
        //        Username = username,
        //        Password = password,
        //        BaseUrl = baseUrl
        //    };
        //}

        public IzibizClient()
        {
           
            _authAdapter = new AuthAdapter();
            _eArchiveInvoiceAdapter = new EArchiveInvoiceAdapter();
            _eInvoiceAdapter = new EInvoiceAdapter();
            _eDespatchAdapter = new EDespatchAdapter();
            _eDespatchReceiptAdapter = new EDespatchReceiptAdapter();
            _creditNoteAdapter = new CreditNoteAdapter();
            _eSmmAdapter = new ESmmAdapter();
            _eCurrencyAdapter = new EExchangeAdapter();
            _eTicketAdapter = new ECheckAdapter();
        }


        public AuthAdapter Auth()
        {
            return _authAdapter;
        }

        public EArchiveInvoiceAdapter EArchiveInvoice()
        {
            return _eArchiveInvoiceAdapter;
        }

        public EInvoiceAdapter EInvoice()
        {
            return _eInvoiceAdapter;
        }

        public EDespatchAdapter EDespatch()
        {
            return _eDespatchAdapter;
        }

        public EDespatchReceiptAdapter EDespatchReceipt()
        {
            return _eDespatchReceiptAdapter;
        }

        public CreditNoteAdapter CreditNote()
        {
            return _creditNoteAdapter;
        }

        public ESmmAdapter ESmm()
        {
            return _eSmmAdapter;
        }

        public EExchangeAdapter EExchange()
        {
            return _eCurrencyAdapter;
        }

        public ECheckAdapter ECheck()
        {
            return _eTicketAdapter;
        }
    }
}
