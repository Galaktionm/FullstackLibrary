

using LibraryApp.API.Data.Entities;

public class DatabaseSeeder {

    public DatabaseContext dbContext;

    public DatabaseSeeder(DatabaseContext databaseContext){
        this.dbContext=databaseContext;
    }

    public  void loadData(){

    //Book book1=new Book("Fundamentals of business", "Educational book", 3);
    //Book book2=new Book("Somehow I Manage", "Management book", 2);
    //Author author1=new Author("Michael Scott");
    //author1.addBook(book1);
    //author1.addBook(book2);

    Book book3=new Book("Salem's lot", "Thousands of miles away from the small township of 'Salem's Lot, two terrified people, a man and a boy, still share the secrets of those clapboard houses and tree-lined streets. They must return to 'Salem's Lot for a final confrontation with the unspeakable evil that lives on in the town.", 5);
    Book book4=new Book("Pet Sematary", "When the Creeds move into a beautiful old house in rural Maine, it all seems too good to be true: physician father, beautiful wife, charming little daughter, adorable infant son-and now an idyllic home. As a family, they've got it all...right down to the friendly car. But the nearby woods hide a blood-chilling truth-more terrifying than death itself-and hideously more powerful. The Creeds are going to learn that sometimes dead is better.", 3);
    Book book5=new Book("Misery", "Paul Sheldon. He's a bestselling novelist who has finally met his biggest fan. Her name is Annie Wilkes and she is more than a rabid reader - she is Paul's nurse, tending his shattered body after an automobile accident. But she is also his captor, keeping him prisoner in her isolated house.", 8);
    Author author2=new Author("Stephen King");
    author2.addBook(book3);
    author2.addBook(book4);
    author2.addBook(book5);

    Book book6=new Book("The Murder of Roger Ackroyd", "Considered to be one of Agatha Christie's greatest, and also most controversial mysteries, 'The Murder Of Roger Ackroyd' breaks the rules of traditional mystery. The peaceful English village of King’s Abbot is stunned. The widow Ferrars dies from an overdose of Veronal. Not twenty-four hours later, Roger Ackroyd—the man she had planned to marry—is murdered. It is a baffling case involving blackmail and death that taxes Hercule Poirot’s “little grey cells” before he reaches one of the most startling conclusions of his career.", 5);
    Book book7=new Book("Murder on the Orient Express", "Just after midnight, a snowdrift stops the famous Orient Express in its tracks as it travels through the mountainous Balkans. The luxurious train is surprisingly full for the time of the year but, by the morning, it is one passenger fewer. An American tycoon lies dead in his compartment, stabbed a dozen times, his door locked from the inside. One of the passengers is none other than detective Hercule Poirot. On vacation. Isolated and with a killer on board, Poirot must identify the murderer—in case he or she decides to strike again.", 3);
    Book book8=new Book("The big four", "Framed in the doorway of Poirot’s bedroom stood an uninvited guest, coated from head to foot in dust. The man’s gaunt face stared for a moment, then he swayed and fell. Who was he? Was he suffering from shock or just exhaustion? Above all, what was the significance of the figure 4, scribbled over and over again on a sheet of paper? We follow Hercule Poirot as he finds himself plunged into a world of international intrigue, risking his life to uncover the truth about ‘Number Four’.", 2);
    Author author3=new Author("Agatha Christie");
    author3.addBook(book6);
    author3.addBook(book7);
    author3.addBook(book8);

    Book book9=new Book("The Children of Captain Grant", "ALL that could be discovered, however, on these pieces of paper was a few words here and there, the remainder of the lines being almost completely obliterated by the action of the water. Lord Glenarvan examined them attentively for a few minutes, turning them over on all sides, holding them up to the light, and trying to decipher the least scrap of writing, while the others looked on with anxious eyes.", 6);
    Book book10=new Book("The Mysterious Island", "After hijacking a balloon from a Confederate camp, a band of five northern prisoners escapes the American Civil War. Seven thousand miles later, they drop from the clouds onto an uncharted volcanic island in the Pacific. Through teamwork, scientific knowledge, engineering, and perseverance, they endeavour to build a colony from scratch. But this island of abundant resources has its secrets. The castaways discover they are not alone. A shadowy, yet familiar, agent of their unfathomable fate is watching.", 3);
    Author author4=new Author("Jules Verne");
    author4.addBook(book9);
    author4.addBook(book10);


    //dbContext.Add(author1);
    dbContext.Add(author2);
    dbContext.Add(author3);
    dbContext.Add(author4);
    dbContext.SaveChanges();
        

    }





}