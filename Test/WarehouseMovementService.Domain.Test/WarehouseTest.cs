using Moq;
using Shouldly;
using WarehouseMovementService.Domain.DDD;

namespace WarehouseMovementService.Domain.Test
{
    [TestClass]
    public class WarehouseTest
    {
        [TestMethod]
        public async Task CreateWarehouse()
        {
            IWarehouseRepository repository = new Mock<IWarehouseRepository>().Object;
            WarehouseService service = new WarehouseService(repository);

            Warehouse warehouse = await service.CreateWarehouse("test");

            warehouse.Code.ShouldBe("test");
        }

        [TestMethod]
        public async Task CreateWarehouse_CodeAlreadyPresent()
        {
            Warehouse warehouse = await CreateTestWarehouse();

            Mock<IWarehouseRepository> repositoryMock = new Mock<IWarehouseRepository>();

            repositoryMock
                        .Setup(r => r.FindWarehouseByCode("test"))
                        .ReturnsAsync(warehouse);

            WarehouseService service = new WarehouseService(repositoryMock.Object);

            (await service.CreateWarehouse("test")).ShouldBe(warehouse);
        }

        [TestMethod]
        public async Task LoadProduct_EmptyWarehouse()
        {
            Warehouse warehouse = await CreateTestWarehouse();

            Guid productID = Guid.NewGuid();

            warehouse.LoadProduct(productID, 5);

            warehouse
                .Availabilities
                .Any(a => a.ProductID == productID && a.Quantity == 5)
                .ShouldBeTrue();
        }

        [TestMethod]
        public async Task LoadProduct_AlreadyPresentProduct()
        {
            Warehouse warehouse = await CreateTestWarehouse();

            Guid productID = Guid.NewGuid();

            warehouse.LoadProduct(productID, 5);
            warehouse.LoadProduct(productID, 5);

            warehouse
                .Availabilities
                .Any(a => a.ProductID == productID && a.Quantity == 10)
                .ShouldBeTrue();
        }

        /*
         * NOTE
         * You can replace with Builder pattern.
         * In this case is really easy so also a method can be good
         */
        private async Task<Warehouse> CreateTestWarehouse()
        {
            IWarehouseRepository repositoryToCreate = new Mock<IWarehouseRepository>().Object;
            WarehouseService serviceToCreate = new WarehouseService(repositoryToCreate);
            return await serviceToCreate.CreateWarehouse("test");
        }
    }
}