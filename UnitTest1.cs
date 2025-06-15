using ClassLibrary2;
using System.Collections.ObjectModel;

[TestClass]
public class MyObservableCollectionTests
{
    [TestMethod]
    public void Constructor_WithName_InitializesCorrectly()
    {
        // Arrange & Act
        var collection = new MyObservableCollection<BankCard>("TestCollection");

        // Assert
        Assert.AreEqual("TestCollection", collection.Name);
        Assert.AreEqual(0, collection.Count);
    }

    [TestMethod]
    public void Constructor_WithNameAndLength_InitializesWithItems()
    {
        // Arrange & Act
        var collection = new MyObservableCollection<BankCard>("TestCollection", 3);

        // Assert
        Assert.AreEqual("TestCollection", collection.Name);
        Assert.AreEqual(3, collection.Count);
    }

    [TestMethod]
    public void Add_Item_RaisesCountChangedEvent()
    {
        // Arrange
        var collection = new MyObservableCollection<BankCard>("TestCollection");
        var raised = false;
        collection.CollectionCountChanged += (s, e) => raised = true;

        // Act
        collection.Add(new BankCard());

        // Assert
        Assert.IsTrue(raised);
    }

    [TestMethod]
    public void Remove_ExistingItem_RaisesCountChangedEvent()
    {
        // Arrange
        var card = new BankCard();
        var collection = new MyObservableCollection<BankCard>("TestCollection");
        collection.Add(card);
        var raised = false;
        collection.CollectionCountChanged += (s, e) => raised = true;

        // Act
        collection.Remove(card);

        // Assert
        Assert.IsTrue(raised);
    }

    [TestMethod]
    public void Remove_NonExistingItem_ReturnsFalse()
    {
        // Arrange
        var collection = new MyObservableCollection<BankCard>("TestCollection");

        // Act
        var result = collection.Remove(new BankCard());

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Clear_NonEmptyCollection_RaisesCountChangedEvent()
    {
        // Arrange
        var collection = new MyObservableCollection<BankCard>("TestCollection", 2);
        var raised = false;
        collection.CollectionCountChanged += (s, e) => raised = true;

        // Act
        collection.Clear();

        // Assert
        Assert.IsTrue(raised);
        Assert.AreEqual(0, collection.Count);
    }

    [TestMethod]
    public void Indexer_SetExistingItem_RaisesReferenceChangedEvent()
    {
        // Arrange
        var oldCard = new BankCard();
        var newCard = new BankCard();
        var collection = new MyObservableCollection<BankCard>("TestCollection");
        collection.Add(oldCard);
        var raised = false;
        collection.CollectionReferenceChanged += (s, e) => raised = true;

        // Act
        collection[oldCard] = newCard;

        // Assert
        Assert.IsTrue(raised);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void Indexer_SetNonExistingItem_ThrowsException()
    {
        // Arrange
        var collection = new MyObservableCollection<BankCard>("TestCollection");

        // Act
        collection[new BankCard()] = new BankCard();
    }

    [TestMethod]
    public void Indexer_GetExistingItem_ReturnsItem()
    {
        // Arrange
        var card = new BankCard();
        var collection = new MyObservableCollection<BankCard>("TestCollection");
        collection.Add(card);

        // Act
        var result = collection[card];

        // Assert
        Assert.AreEqual(card, result);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void Indexer_GetNonExistingItem_ThrowsException()
    {
        // Arrange
        var collection = new MyObservableCollection<BankCard>("TestCollection");

        // Act
        var result = collection[new BankCard()];
    }
}