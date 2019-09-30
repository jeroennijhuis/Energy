using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Globalization;

namespace Energy.Repository.DynamoDb.Converters
{
    public class DateTimeConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            if (!DateTime.TryParse(entry.ToString(), out var value))
                throw new ArgumentException("entry must be a valid DateTime value.", nameof(entry));

            return value;
        }

        public DynamoDBEntry ToEntry(object value)
        {
            if (value.GetType() != typeof(DateTime))
                throw new ArgumentException("value must be a DateTime.",
                    nameof(value));
            return ((DateTime)value).ToString(CultureInfo.InvariantCulture);
        }
    }
}