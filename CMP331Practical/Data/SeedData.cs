using CMP331Practical.Contracts;
using CMP331Practical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace CMP331Practical.Data
{
    class SeedData
    {
        IRepository<Invoice> invoiceContext;
        IRepository<Property> propertyContext;
        IRepository<Role> roleContext;
        IRepository<User> userContext;

        public SeedData()
        {
            invoiceContext = ContainerHelper.Container.Resolve<IRepository<Invoice>>();
            propertyContext = ContainerHelper.Container.Resolve<IRepository<Property>>();
            roleContext = ContainerHelper.Container.Resolve<IRepository<Role>>();
            userContext = ContainerHelper.Container.Resolve<IRepository<User>>();

            Console.WriteLine("*************************************************************");
            Console.WriteLine("** WARNING: Remember, you should only run seed method once **");
            Console.WriteLine("*************************************************************");
        }

        public async void AddData()
        {
            Property propertyOne = new Property(true, "123 Fake Street", "", "NE1 ABC", (float) 575.00 , "Gas", new DateTime(), new DateTime(), new DateTime(), null, null) ;
            propertyContext.Insert(propertyOne);
            

            Console.WriteLine("Property Data successfully seeded.");

            Role roleOne = new Role("System Admin");
            roleContext.Insert(roleOne);
            Role roleTwo = new Role("Letting Agent");
            roleContext.Insert(roleTwo);
            Role roleThree = new Role("Maintainance - Plumber");
            roleContext.Insert(roleThree);
            Role roleFour = new Role("Maintainance - Gas");
            roleContext.Insert(roleFour);
            Role roleFive = new Role("Maintainance - Heating");
            roleContext.Insert(roleFive);
            Role roleSix = new Role("Maintainance - Electrician");
            roleContext.Insert(roleSix);

            await roleContext.Commit();
            Console.WriteLine("Role data successfully seeded");

            User userOne = new User("John", "Doe", "admin@test.com","Password12", roleOne.Id);
            userContext.Insert(userOne);
            User userTwo = new User("Jane", "Doe", "agent@test.com", "Password12", roleTwo.Id);
            userContext.Insert(userTwo);
            User userThree = new User("Test", "McPlumber", "plumber@test.com", "Password12", roleThree.Id);
            userContext.Insert(userThree);
            User userFour = new User("Test", "McGas", "gas@test.com", "Password12", roleFour.Id);
            userContext.Insert(userFour);
            User userFive = new User("Test", "McHeating", "heating@test.com", "Password12", roleFive.Id);
            userContext.Insert(userFive);
            User userSix = new User("Test", "McElectric", "electrician@test.com", "Password12", roleSix.Id);
            userContext.Insert(userSix);

            await userContext.Commit();
            Console.WriteLine("User data successfully seeded.");
        }
    }
}
