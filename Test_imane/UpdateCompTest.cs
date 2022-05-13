using System;
using Xunit;
using Bunit;
using Components ;

namespace Test_composant_imane {

    public class UpdateComponentTest
    {
        private Bunit.TestContext? ctx;
        
        [Fact]
        public void UpdateComponent_ValidDatesParameters_Component()
        {
            //Arrange
            /** Initializes a new instance of the TestContext class.
             * A test context is a factory that makes it possible to create components under tests.
            **/
            ctx = new TestContext();
                
            //Act
            //Instantiates and performs a first render of a component of type UpdateComponent.
            var cut = ctx.RenderComponent<UpdateComponent>(parameters => parameters
                .Add (p=> p.DateStart, DateTime.Now.AddDays(-10))
                .Add (p=> p.DateEnd,  DateTime.Now.AddDays(10))
            ); 
        }

        [Fact]
        public void UpdateComponent_DateEndLessDateStart_ArgumentException()
        {
            try {
                //Arrange
                ctx = new TestContext();
                
                //Act
                var cut = ctx.RenderComponent<UpdateComponent>(parameters => parameters
                    .Add (p=> p.DateStart, DateTime.Now.AddDays(10))
                    .Add (p=> p.DateEnd,  DateTime.Now.AddDays(9))
                ); 

            //Assert
            }catch(ArgumentException ae)
            {
                //Compare the first string with the error message expected
                Assert.Equal("end date cannot be less than start date", ae.Message);
            } 
        }

        [Fact]
        public void UpdateComponent_DateEndLessTodayDate_ArgumentException()
        {
            try {
                //Arrange
                ctx = new TestContext();
                
                //Act
                var cut = ctx.RenderComponent<UpdateComponent>(parameters => parameters
                    .Add (p=> p.DateStart, DateTime.Now.AddDays(-10))
                    .Add (p=> p.DateEnd,  DateTime.Now.AddDays(-1))
                ); 

            //Assert
            }catch(ArgumentException ae)
            {
                Assert.Equal("end date cannot be less than today date", ae.Message);
            }
        }

        [Fact]
        public void UpdateComponent_DateStartHigherTodayDate_ArgumentException()
        {
            try {
                //Arrange
                ctx = new TestContext();
                
                //Act
                var cut = ctx.RenderComponent<UpdateComponent>(parameters => parameters
                    .Add (p=> p.DateStart, DateTime.Now.AddDays(1))
                    .Add (p=> p.DateEnd,  DateTime.Now.AddDays(10))
                ); 

            //Assert
            }catch(ArgumentException ae)
            {
                Assert.Equal("start date cannot be higher than today date", ae.Message);
            }
        } 

        [Fact]
        public void UpdateComponent_DateStartEqualDateEnd_ArgumentException()
        {
            try {
                //Arrange
                ctx = new TestContext();
                DateTime d = DateTime.Now;
                //Act
                var cut = ctx.RenderComponent<UpdateComponent>(parameters => parameters
                    .Add (p=> p.DateStart, d)
                    .Add (p=> p.DateEnd,  d)
                ); 

            //Assert
            }catch(ArgumentException ae)
            {
                Assert.Equal("start date cannot be equal to end date", ae.Message);
            }
        } 
    }
}