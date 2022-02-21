using System;
using System.Linq;
using CoreBusinessLibrary;
using Csla;
using NUnit.Framework;

namespace CoreBusinessLibraryDotNetTests
{
    [TestFixture]
    public class No_Space_Allowed_BR_Tests
    {
        [Test]
        public void When_String_Is_Blank_Then_Allow()
        {
            var obj = DataPortal.Create<EditObj>();
            Assert.IsTrue(!obj.BrokenRulesCollection.Any());

            obj.Description = "";

            Assert.IsTrue(!obj.BrokenRulesCollection.Any());
        }

        [TestCase("123")]
        [TestCase("abc")]
        [TestCase("aB")]
        [TestCase("aBcD123!")]
        public void When_String_Does_Not_Contain_Space_Then_Allow(string value)
        {
            var obj = DataPortal.Create<EditObj>();
            Assert.IsTrue(!obj.BrokenRulesCollection.Any());

            obj.Description = value;

            Assert.IsTrue(!obj.BrokenRulesCollection.Any());
        }

        [TestCase(" 123")]
        [TestCase(" Value")]
        public void When_String_Starts_With_Space_Then_Disallow(string value)
        {
            var obj = DataPortal.Create<EditObj>();
            
            obj.Description = value;

            Assert.IsTrue(obj.BrokenRulesCollection.Any());
        }

        [TestCase("ABC ")]
        [TestCase("ABCdef   ")]
        public void When_String_Ends_With_Space_Then_Disallow(string value)
        {
            var obj = DataPortal.Create<EditObj>();

            obj.Description = value;

            Assert.IsTrue(obj.BrokenRulesCollection.Any());
        }

        [TestCase("A b")]
        [TestCase("ABC   def")]
        public void When_String_Contains_Space_Then_Disallow(string value)
        {
            var obj = DataPortal.Create<EditObj>();

            obj.Description = value;

            Assert.IsTrue(obj.BrokenRulesCollection.Any());
        }
    }


    [Serializable]
    public class EditObj : BusinessBase<EditObj>
    {

        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(nameof(Description));
       
        public string Description
        {
            get => GetProperty(DescriptionProperty);
            set => SetProperty(DescriptionProperty, value);
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new NoSpaceAllowed(DescriptionProperty));
        }
    }

   
}
