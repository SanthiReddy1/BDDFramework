using System;
using System.Collections.Generic;
using System.Linq;

namespace SnapDealTestProject.Utils.Assertions
{
    using System.Text;

    using NUnit.Framework;

    using SnapDealTestProject.Utils.Enum;

    using TechTalk.SpecFlow;

    public class QualityTestCase
    {
        /// <summary>
        /// Gets or sets the list of Quality Checks.
        /// </summary>
        /// <value>The list of Quality Checks.</value>
        public IList<QualityCheck> QualityChecks = new List<QualityCheck>();

        /// <summary>
        /// Adds a new QualityCheck to the QualityTestCase.
        /// </summary>
        /// <param name="checkName">The name of the new check being added.</param>
        /// <returns>The QualityCheck that has been newly-added to the QualityTestCase. </returns>
        /// <exception cref="ArgumentException">Thrown if a duplicate check is already present</exception>
        public QualityCheck AddCheck(string checkName)
        {
            if (this.HasCheck(checkName))
            {
                throw new ArgumentException(
                    string.Format("A quality check with name '{0}' already exists in the collection.", checkName));
            }

            var newCheck = new QualityCheck(checkName);
            this.QualityChecks.Add(newCheck);
            return newCheck;
        }

        /// <summary>
        /// Adds multiple QualityCheck objects to the list of QualityCheck objects for a QualityTestCase.
        /// </summary>
        /// <param name="checks">A comma-delimited list of QualityCheck objects.</param>
        /// <exception cref="ArgumentException">Thrown if a duplicate check is already present</exception>
        public void AddQualityChecks(params QualityCheck[] checks)
        {
            foreach (var check in checks)
            {
                if (this.QualityChecks.Any(qc => qc.Name.Equals(check.Name, StringComparison.InvariantCulture)))
                {
                    throw new ArgumentException(
                        string.Format("A quality check with name '{0}' already exists in the collection.", check.Name));
                }

                this.QualityChecks.Add(check);
            }

        }

        /// <summary>
        /// Retrieves the QualityCheck from the QualityTestCase with the specified name,
        /// primarily for backwards compatibility in older test suites
        /// </summary>
        /// <param name="checkName">the name of the QualityCheck</param>
        /// <returns>the corresponding QualityCheck</returns>
        /// <exception cref="ArgumentException">thrown if a QualityCheck cannot be forund with the specified name</exception>
        public QualityCheck GetQualityCheckByName(string checkName)
        {
            return this.GetCheck(checkName);
        }

        /// <summary>
        /// Checks to see if a QualityCheck from the QualityTestCase has the specified name.
        /// </summary>
        /// <param name="checkName">The name of the QualityCheck.</param>
        /// <returns>True if a QualityCheck from the QualityTestCase has the specified name and false if it doesn't.</returns>
        public bool HasCheck(string checkName)
        {
            try
            {
                return (this.GetCheck(checkName) != null);
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves the QualityCheck from the QualityTestCase with the specified name,
        /// primarily for backwards compatibility in older test suites
        /// </summary>
        /// <param name="checkName">the name of the QualityCheck</param>
        /// <returns>the corresponding QualityCheck</returns>
        /// <exception cref="ArgumentException">thrown if a QualityCheck cannot be forund with the specified name</exception>
        public QualityCheck GetCheck(string checkName)
        {
            foreach (var check in this.QualityChecks)
            {
                if (check.Name.Equals(checkName))
                {
                    return check;
                }
            }
            throw new ArgumentException("No QualityCheck could be found corresponding to the name: \'" + checkName +
                                        "\'.");
        }

        public void Cleanup(ScenarioContext scenarioContext)
        {
            // determine whether any verifications failed, and fail the test case, if that occurs
            IList<QualityCheck> failedQualityChecks =
                this.QualityChecks.Where(qc => qc.Outcome == Outcome.Failed).ToList();
            if (failedQualityChecks.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var failedQualityCheck in failedQualityChecks)
                {
                    sb.AppendLine(failedQualityCheck.ToString());
                }
                Assert.Fail(sb.ToString());
            }

            var scenarioExecutionStatus = scenarioContext.ScenarioExecutionStatus;

            // examine remaining verifications only if an exception has not occurred during the test case; hence, check for Passed
            if (scenarioExecutionStatus == ScenarioExecutionStatus.OK ||
            scenarioExecutionStatus == ScenarioExecutionStatus.StepDefinitionPending)
            {
                // determine whether any verifications are still inconclusive, and mark the test case inconclusive, if that occurs
                IList<QualityCheck> inconclusiveQualityChecks =
                    this.QualityChecks.Where(qc => qc.Outcome == Outcome.Inconclusive).ToList();
                if (inconclusiveQualityChecks.Count > 0)
                {
                    var sb = new StringBuilder();
                    foreach (var inconclusiveQualityCheck in inconclusiveQualityChecks)
                    {
                        sb.AppendLine(inconclusiveQualityCheck.ToString());
                    }
                    Assert.Inconclusive(sb.ToString());
                }
            }
        }
    }
}
