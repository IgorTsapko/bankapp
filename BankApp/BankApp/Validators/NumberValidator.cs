using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BankApp.Validators
{
    class NumberValidator : Behavior<Entry>
    {
        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(NumberValidator), false);
        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public int MaxVal { get; set; }
 

        public int MinVal { get; set; }
 
        public int MaxLen { get; set; }

        public int MinLen { get; set; }
    
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            private set => SetValue(IsValidPropertyKey, value);
        }
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                var curVal = e.NewTextValue;
                bool allDigits = true;
                bool maxLenOk = true;
                bool minLenOk = true;
                bool maxValOk = true;
                bool minValOk = true;

                for (int i = 0; i < curVal.Length; i++)
                    allDigits = allDigits && Char.IsDigit(curVal, i);

                if (MaxLen > 0)
                    maxLenOk = curVal.Length <= MaxLen;

                if (MinLen > 0)
                    minLenOk = curVal.Length >= MinLen;

                if (MaxVal > 0)
                {
                    maxValOk = Int32.TryParse(curVal, out int val) && val <= MaxVal;
                }

                if (MinVal > 0)
                    minValOk = Int32.TryParse(curVal, out int val) && val >= MinVal;

                IsValid = allDigits && maxLenOk && minLenOk && maxValOk && minValOk;
            }
            catch (Exception ex)
            {
                Other.ExceptionProcessor.ProcessException(ex);
                IsValid = false;
            }

            ((Entry)sender).TextColor = IsValid ? Color.Green : Color.Red;

        }
    }
}
