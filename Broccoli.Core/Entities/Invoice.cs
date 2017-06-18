﻿using Broccoli.Core.Database.Eloquent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broccoli.Core.Entities
{
    [PetaPoco.TableName("sales__invoice")]
    [PetaPoco.ExplicitColumns]
    public class Invoice : Model<Invoice>,IDisposable
    {
        public Invoice()
        {
            ModelSavedEvent += Invoice_ModelSavedEvent;
        }

        
        private void Invoice_ModelSavedEvent(object sender, Database.Events.ModelChangedEventArgs<Invoice> e)
        {
            Customers.Save(this);
        }

        public void Dispose()
        {
        }

        [PetaPoco.Column("invoice_num")]
        public string InvoiceNum
        {
            get
            {
                return Get<string>();
            }
            set
            {
                Set<string>(value);
            }
        }

        [PetaPoco.Column("invoice_datetime")]
        public DateTime? InvoiceDateTime
        {
            get
            {
                return Get<DateTime?>();
            }
            set
            {
                Set<DateTime?>(value);
            }
        }

        private IEnumerable<Customer> _customers;

        [PetaPoco.Ignore]
        public IEnumerable<Customer> Customers
        {
            get
            {
                if (_customers == null)
                {
                    _customers = hasMany<Customer>();
                }
                return _customers;
            }
        }
    }
}
