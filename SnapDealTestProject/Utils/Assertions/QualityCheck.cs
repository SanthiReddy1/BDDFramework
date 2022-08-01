using System;

namespace SnapDealTestProject.Utils.Assertions
{
    using NUnit.Framework;

    using SnapDealTestProject.Utils.Enum;

    public class QualityCheck
    {
        private string _name;

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public Outcome Outcome { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the QualityCheck class and specifies the name.
        /// </summary>
        /// <param name="name">A name for the Quality Check.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided name is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the provided name is an empty string after being trimmed or its length exceeds 256 characters.</exception>
        public QualityCheck(string name)
        {
            Name = name;
            Outcome = Outcome.Inconclusive;
        }

        /// <summary>
        /// Sets this Quality Check to have an Outcome of Passed.
        /// </summary>
        internal void SetSuccessfulValues()
        {
            this.Outcome = Outcome.Passed;
        }

        /// <summary>
        /// Sets this Quality Check to have an Outcome of Failed.
        /// </summary>
        /// <param name="message">A message associated with the failure of this Quality Check.</param>
        internal void SetFailedValues(string message)
        {
            this.Outcome = Outcome.Failed;
            this.Message = message;
        }
    }
}
