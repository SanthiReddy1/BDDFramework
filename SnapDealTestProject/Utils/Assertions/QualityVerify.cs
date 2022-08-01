using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapDealTestProject.Utils.Assertions
{
    public class QualityVerify
    {
        /// <summary>
        /// Verifies that the specified condition is <c>true</c>. The verification fails if the condition is <c>false</c>. Displays a message if the verification fails.
        /// </summary>
        /// <param name="qualityCheck">A Quality Check to store the outcome of the verification.</param>
        /// <param name="condition">The condition to verify is <c>true</c>.</param>
        /// <param name="message">A message to display if the verification fails. This message can be seen in the unit test results.</param>
        /// <returns>An indication whether the verification does not fail.</returns>
        public static bool IsTrue(QualityCheck qualityCheck, bool condition, string message = null)
        {
            try
            {
                Assert.IsTrue(condition, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (Exception afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }
        
        public static bool IsFalse(QualityCheck qualityCheck, bool condition, string message = null)
        {
            try
            {
                Assert.IsFalse(condition, message);
                qualityCheck.SetSuccessfulValues();
                return true;
            }
            catch (Exception afe)
            {
                qualityCheck.SetFailedValues(afe.Message);
                return false;
            }
        }
    }
}
