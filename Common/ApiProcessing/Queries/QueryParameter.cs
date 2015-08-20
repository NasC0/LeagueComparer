using System;

namespace ApiProcessing.Queries
{
    public class QueryParameter
    {
        private string name;
        private string value;

        public QueryParameter(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name value cannot be null, emtpy or plain whitespace.");
                }

                this.name = value;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name value cannot be null, emtpy or plain whitespace.");
                }

                this.value = value;
            }
        }
    }
}
