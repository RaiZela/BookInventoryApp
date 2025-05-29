namespace BookInventoryApp.Data.Seed;

public class AuthorSeedingData
{
    public static void SeedAlbanianWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Rreze", MiddleName = null, LastName = "Abdullahu" },
        new Author { Id = Guid.NewGuid(), Name = "Dritëro", MiddleName = null, LastName = "Agolli" },
        new Author { Id = Guid.NewGuid(), Name = "Mimoza", MiddleName = null, LastName = "Ahmeti" },
        new Author { Id = Guid.NewGuid(), Name = "Ylljet", MiddleName = null, LastName = "Aliçka" },
        new Author { Id = Guid.NewGuid(), Name = "Gëzim", MiddleName = null, LastName = "Alpion" },
        new Author { Id = Guid.NewGuid(), Name = "Valdete", MiddleName = null, LastName = "Antoni" },
        new Author { Id = Guid.NewGuid(), Name = "Fatos", MiddleName = null, LastName = "Arapi" },
        new Author { Id = Guid.NewGuid(), Name = "Lindita", MiddleName = null, LastName = "Arapi" },
        new Author { Id = Guid.NewGuid(), Name = "Pjetër", MiddleName = null, LastName = "Arbnori" },
        new Author { Id = Guid.NewGuid(), Name = "Asdreni", MiddleName = null, LastName = null },
        new Author { Id = Guid.NewGuid(), Name = "Frang", MiddleName = null, LastName = "Bardhi" },
        new Author { Id = Guid.NewGuid(), Name = "Marin", MiddleName = null, LastName = "Barleti" },
        new Author { Id = Guid.NewGuid(), Name = "Eqrem", MiddleName = null, LastName = "Basha" },
        new Author { Id = Guid.NewGuid(), Name = "Mario", MiddleName = null, LastName = "Bellizzi" },
        new Author { Id = Guid.NewGuid(), Name = "Nafiz", MiddleName = null, LastName = "Bezhani" },
        new Author { Id = Guid.NewGuid(), Name = "Ben", MiddleName = null, LastName = "Blushi" },
        new Author { Id = Guid.NewGuid(), Name = "Pjetër", MiddleName = null, LastName = "Bogdani" },
        new Author { Id = Guid.NewGuid(), Name = "Flora", MiddleName = null, LastName = "Brovina" },
        new Author { Id = Guid.NewGuid(), Name = "Maria Antonia", MiddleName = null, LastName = "Braile" },
        new Author { Id = Guid.NewGuid(), Name = "Dionis", MiddleName = null, LastName = "Bubani" },
        new Author { Id = Guid.NewGuid(), Name = "Gjergj", MiddleName = null, LastName = "Bubani" },
        new Author { Id = Guid.NewGuid(), Name = "Klara", MiddleName = null, LastName = "Buda" },
        new Author { Id = Guid.NewGuid(), Name = "Pjetër", MiddleName = null, LastName = "Budi" },
        new Author { Id = Guid.NewGuid(), Name = "Uran", MiddleName = null, LastName = "Butka" },
        new Author { Id = Guid.NewGuid(), Name = "Gjon", MiddleName = null, LastName = "Buzuku" },
        new Author { Id = Guid.NewGuid(), Name = "Martin", MiddleName = null, LastName = "Camaj" },
        new Author { Id = Guid.NewGuid(), Name = "Nicola", MiddleName = null, LastName = "Chetta" },
        new Author { Id = Guid.NewGuid(), Name = "Selfixhe", MiddleName = null, LastName = "Ciu" },
        new Author { Id = Guid.NewGuid(), Name = "Constantine", MiddleName = null, LastName = "of Berat" },
        new Author { Id = Guid.NewGuid(), Name = "Nelson", MiddleName = null, LastName = "Çabej" },
        new Author { Id = Guid.NewGuid(), Name = "Aleks", MiddleName = null, LastName = "Çaçi" },
        new Author { Id = Guid.NewGuid(), Name = "Andon Zako", MiddleName = null, LastName = "Çajupi" },
        new Author { Id = Guid.NewGuid(), Name = "Thoma", MiddleName = null, LastName = "Çami" },
        new Author { Id = Guid.NewGuid(), Name = "Spiro", MiddleName = null, LastName = "Çomora" },
        new Author { Id = Guid.NewGuid(), Name = "Diana", MiddleName = null, LastName = "Çuli" },
        new Author { Id = Guid.NewGuid(), Name = "Gavril", MiddleName = "Dara", LastName = "the Younger" },
        new Author { Id = Guid.NewGuid(), Name = "Adem", MiddleName = null, LastName = "Demaçi" },
        new Author { Id = Guid.NewGuid(), Name = "Musa", MiddleName = null, LastName = "Demi" },
        new Author { Id = Guid.NewGuid(), Name = "Jeronim", MiddleName = null, LastName = "De Rada" },
        new Author { Id = Guid.NewGuid(), Name = "Ridvan", MiddleName = null, LastName = "Dibra" },
        new Author { Id = Guid.NewGuid(), Name = "Dora", MiddleName = null, LastName = "d'Istria" },
        new Author { Id = Guid.NewGuid(), Name = "Spiro", MiddleName = null, LastName = "Dine" },
        new Author { Id = Guid.NewGuid(), Name = "Elvira", MiddleName = null, LastName = "Dones" },
        new Author { Id = Guid.NewGuid(), Name = "Yahya", MiddleName = null, LastName = "Dukagjini" },
        new Author { Id = Guid.NewGuid(), Name = "Pal", MiddleName = null, LastName = "Engjëlli" },
        new Author { Id = Guid.NewGuid(), Name = "Rudi", MiddleName = null, LastName = "Erëbara" },
        new Author { Id = Guid.NewGuid(), Name = "Nikollë", MiddleName = null, LastName = "Filja" },
        new Author { Id = Guid.NewGuid(), Name = "Gjergj", MiddleName = null, LastName = "Fishta" },
        new Author { Id = Guid.NewGuid(), Name = "Nezim", MiddleName = null, LastName = "Frakulla" },
        new Author { Id = Guid.NewGuid(), Name = "Dalip", MiddleName = null, LastName = "Frashëri" },
        new Author { Id = Guid.NewGuid(), Name = "Naim", MiddleName = null, LastName = "Frashëri" },
        new Author { Id = Guid.NewGuid(), Name = "Sami", MiddleName = null, LastName = "Frashëri" },
        new Author { Id = Guid.NewGuid(), Name = "Shahin", MiddleName = null, LastName = "Frashëri" },
        new Author { Id = Guid.NewGuid(), Name = "Llazar", MiddleName = null, LastName = "Fundo" },
        new Author { Id = Guid.NewGuid(), Name = "Mirko", MiddleName = null, LastName = "Gashi" },
        new Author { Id = Guid.NewGuid(), Name = "Sabri", MiddleName = null, LastName = "Godo" },
        new Author { Id = Guid.NewGuid(), Name = "Gregory", MiddleName = "Of", LastName = "Durrës" },
        new Author { Id = Guid.NewGuid(), Name = "Odhise", MiddleName = null, LastName = "Grillo" },
        new Author { Id = Guid.NewGuid(), Name = "Luigj", MiddleName = null, LastName = "Gurakuqi" },
        new Author { Id = Guid.NewGuid(), Name = "Fatmir", MiddleName = null, LastName = "Gjata" },
        new Author { Id = Guid.NewGuid(), Name = "Kadri", MiddleName = null, LastName = "Gjata" },
        new Author { Id = Guid.NewGuid(), Name = "Julia", MiddleName = null, LastName = "Gjika" },
        new Author { Id = Guid.NewGuid(), Name = "Sinan", MiddleName = null, LastName = "Hasani" },
        new Author { Id = Guid.NewGuid(), Name = "Ervin", MiddleName = null, LastName = "Hatibi" },
        new Author { Id = Guid.NewGuid(), Name = "Rifat", MiddleName = null, LastName = "Hoxha" },
        new Author { Id = Guid.NewGuid(), Name = "Shefki", MiddleName = null, LastName = "Hysa" },
        new Author { Id = Guid.NewGuid(), Name = "Anilda", MiddleName = null, LastName = "Ibrahimi" },
        new Author { Id = Guid.NewGuid(), Name = "Vera", MiddleName = null, LastName = "Isaku" },
        new Author { Id = Guid.NewGuid(), Name = "Nikolla", MiddleName = null, LastName = "Ivanaj" },
        new Author { Id = Guid.NewGuid(), Name = "Halil", MiddleName = null, LastName = "Jaçellari" },
        new Author { Id = Guid.NewGuid(), Name = "Petro", MiddleName = null, LastName = "Janura" },
        new Author { Id = Guid.NewGuid(), Name = "Irhan", MiddleName = null, LastName = "Jubica" },
        new Author { Id = Guid.NewGuid(), Name = "Helena", MiddleName = null, LastName = "Kadare" },
        new Author { Id = Guid.NewGuid(), Name = "Ismail", MiddleName = null, LastName = "Kadare" },
        new Author { Id = Guid.NewGuid(), Name = "Hasan", MiddleName = "Zyko", LastName = "Kamberi" },
        new Author { Id = Guid.NewGuid(), Name = "Veli", MiddleName = null, LastName = "Karahoda" },
        new Author { Id = Guid.NewGuid(), Name = "Amik", MiddleName = null, LastName = "Kasoruho" },
        new Author { Id = Guid.NewGuid(), Name = "Teodor", MiddleName = null, LastName = "Keko" },
        new Author { Id = Guid.NewGuid(), Name = "Jeton", MiddleName = null, LastName = "Kelmendi" },
        new Author { Id = Guid.NewGuid(), Name = "Skifter", MiddleName = null, LastName = "Këlliçi" },
        new Author { Id = Guid.NewGuid(), Name = "Ardian", MiddleName = null, LastName = "Klosi" },
        new Author { Id = Guid.NewGuid(), Name = "Jolanda", MiddleName = null, LastName = "Kodra" },
        new Author { Id = Guid.NewGuid(), Name = "Musine", MiddleName = null, LastName = "Kokalari" },
        new Author { Id = Guid.NewGuid(), Name = "Vedat", MiddleName = null, LastName = "Kokona" },
        new Author { Id = Guid.NewGuid(), Name = "Dashnor", MiddleName = null, LastName = "Kokonozi" },
        new Author { Id = Guid.NewGuid(), Name = "Aristidh", MiddleName = null, LastName = "Kola" },
        new Author { Id = Guid.NewGuid(), Name = "Ernest", MiddleName = null, LastName = "Koliqi" },
        new Author { Id = Guid.NewGuid(), Name = "Anastas", MiddleName = null, LastName = "Kondo" },
        new Author { Id = Guid.NewGuid(), Name = "Fatos", MiddleName = null, LastName = "Kongoli" },
        new Author { Id = Guid.NewGuid(), Name = "Faik", MiddleName = null, LastName = "Konica" },
        new Author { Id = Guid.NewGuid(), Name = "Vath", MiddleName = null, LastName = "Koreshi" },
        new Author { Id = Guid.NewGuid(), Name = "Eulogios", MiddleName = "Kourilas", LastName = "Lauriotis" },
        new Author { Id = Guid.NewGuid(), Name = "Irma", MiddleName = null, LastName = "Kurti" },
        new Author { Id = Guid.NewGuid(), Name = "Mitrush", MiddleName = null, LastName = "Kuteli" },
        new Author { Id = Guid.NewGuid(), Name = "Teodor", MiddleName = null, LastName = "Laço" },
        new Author { Id = Guid.NewGuid(), Name = "Natasha", MiddleName = null, LastName = "Lako" },
        new Author { Id = Guid.NewGuid(), Name = "Skender", MiddleName = null, LastName = "Luarasi" },
        new Author { Id = Guid.NewGuid(), Name = "Fatos", MiddleName = null, LastName = "Lubonja" },
        new Author { Id = Guid.NewGuid(), Name = "Luljeta", MiddleName = null, LastName = "Lleshanaku" },
        new Author { Id = Guid.NewGuid(), Name = "Sejfulla", MiddleName = null, LastName = "Malëshova" },
        new Author { Id = Guid.NewGuid(), Name = "Gjekë", MiddleName = null, LastName = "Marinaj" },
        new Author { Id = Guid.NewGuid(), Name = "Petro", MiddleName = null, LastName = "Marko" },
        new Author { Id = Guid.NewGuid(), Name = "Petrus", MiddleName = null, LastName = "Massarechius" },
        new Author { Id = Guid.NewGuid(), Name = "Lekë", MiddleName = null, LastName = "Matrënga" },
        new Author { Id = Guid.NewGuid(), Name = "Din", MiddleName = null, LastName = "Mehmeti" },
        new Author { Id = Guid.NewGuid(), Name = "Vangjel", MiddleName = null, LastName = "Meksi" },
        new Author { Id = Guid.NewGuid(), Name = "Esad", MiddleName = null, LastName = "Mekuli" },
        new Author { Id = Guid.NewGuid(), Name = "Branko", MiddleName = null, LastName = "Merxhani" },
        new Author { Id = Guid.NewGuid(), Name = "Mesini", MiddleName = null, LastName = string.Empty },
        new Author { Id = Guid.NewGuid(), Name = "Migjeni", MiddleName = null, LastName = string.Empty},
        new Author { Id = Guid.NewGuid(), Name = "Ndre", MiddleName = null, LastName = "Mjeda" },
        new Author { Id = Guid.NewGuid(), Name = "Betim", MiddleName = null, LastName = "Muço" },
        new Author { Id = Guid.NewGuid(), Name = "Besnik", MiddleName = null, LastName = "Mustafaj" },
        new Author { Id = Guid.NewGuid(), Name = "Gjon", MiddleName = null, LastName = "Muzaka" },
        new Author { Id = Guid.NewGuid(), Name = "Faruk", MiddleName = null, LastName = "Myrtaj" },
        new Author { Id = Guid.NewGuid(), Name = "Sulejman", MiddleName = null, LastName = "Naibi" },
        new Author { Id = Guid.NewGuid(), Name = "Kristo", MiddleName = null, LastName = "Negovani" },
        new Author { Id = Guid.NewGuid(), Name = "Ndoc", MiddleName = null, LastName = "Nikaj" },
        new Author { Id = Guid.NewGuid(), Name = "Fan", MiddleName = null, LastName = "Noli" },
        new Author { Id = Guid.NewGuid(), Name = "Majlinda", MiddleName = "Nana", LastName = "Rama" },
        new Author { Id = Guid.NewGuid(), Name = "Fadil", MiddleName = null, LastName = "Paçrami" },
        new Author { Id = Guid.NewGuid(), Name = "Ludmilla", MiddleName = null, LastName = "Pajo" },
        new Author { Id = Guid.NewGuid(), Name = "Vaso", MiddleName = null, LastName = "Pasha" },
        new Author { Id = Guid.NewGuid(), Name = "Arshi", MiddleName = null, LastName = "Pipa" },
        new Author { Id = Guid.NewGuid(), Name = "Aurel", MiddleName = null, LastName = "Plasari" },
        new Author { Id = Guid.NewGuid(), Name = "Ali", MiddleName = null, LastName = "Podrimja" },
        new Author { Id = Guid.NewGuid(), Name = "Lasgush", MiddleName = null, LastName = "Poradeci" },
        new Author { Id = Guid.NewGuid(), Name = "Foqion", MiddleName = null, LastName = "Postoli" },
        new Author { Id = Guid.NewGuid(), Name = "Iljaz", MiddleName = null, LastName = "Prokshi" },
        new Author { Id = Guid.NewGuid(), Name = "Leon", MiddleName = null, LastName = "Qafzezi" },
        new Author { Id = Guid.NewGuid(), Name = "Gjergj", MiddleName = null, LastName = "Qiriazi" },
        new Author { Id = Guid.NewGuid(), Name = "Rexhep", MiddleName = null, LastName = "Qosja" },
        new Author { Id = Guid.NewGuid(), Name = "Kadrush", MiddleName = null, LastName = "Radogoshi" },
        new Author { Id = Guid.NewGuid(), Name = "Luan", MiddleName = null, LastName = "Rama" },
        new Author { Id = Guid.NewGuid(), Name = "Musa", MiddleName = null, LastName = "Ramadani" },
        new Author { Id = Guid.NewGuid(), Name = "Nijazi", MiddleName = null, LastName = "Ramadani" },
        new Author { Id = Guid.NewGuid(), Name = "Francesco", MiddleName = "Antonio", LastName = "Santori" },
        new Author { Id = Guid.NewGuid(), Name = "Zef", MiddleName = null, LastName = "Serembe" },
        new Author { Id = Guid.NewGuid(), Name = "Nokë", MiddleName = null, LastName = "Sinishtaj" },
        new Author { Id = Guid.NewGuid(), Name = "Brikena", MiddleName = null, LastName = "Smajli" },
        new Author { Id = Guid.NewGuid(), Name = "Xhevahir", MiddleName = null, LastName = "Spahiu" },
        new Author { Id = Guid.NewGuid(), Name = "Sterjo", MiddleName = null, LastName = "Spasse" },
        new Author { Id = Guid.NewGuid(), Name = "Luan", MiddleName = null, LastName = "Starova" },
        new Author { Id = Guid.NewGuid(), Name = "Haki", MiddleName = null, LastName = "Stërmilli" },
        new Author { Id = Guid.NewGuid(), Name = "Iliriana", MiddleName = null, LastName = "Sulkuqi" },
        new Author { Id = Guid.NewGuid(), Name = "Halit", MiddleName = null, LastName = "Shamata" },
        new Author { Id = Guid.NewGuid(), Name = "Sokol", MiddleName = null, LastName = "Shameti" },
        new Author { Id = Guid.NewGuid(), Name = "Bashkim", MiddleName = null, LastName = "Shehu" },
        new Author { Id = Guid.NewGuid(), Name = "Filip", MiddleName = null, LastName = "Shiroka" },
        new Author { Id = Guid.NewGuid(), Name = "Stefan", MiddleName = null, LastName = "Shundi" },
        new Author { Id = Guid.NewGuid(), Name = "Dhimitër", MiddleName = null, LastName = "Shuteriqi" },
        new Author { Id = Guid.NewGuid(), Name = "Skënder", MiddleName = null, LastName = "Temali" },
        new Author { Id = Guid.NewGuid(), Name = "Ismet", MiddleName = null, LastName = "Toto" },
        new Author { Id = Guid.NewGuid(), Name = "Kasëm", MiddleName = null, LastName = "Trebeshina" },
        new Author { Id = Guid.NewGuid(), Name = "Vorea", MiddleName = null, LastName = "Ujko" },
        new Author { Id = Guid.NewGuid(), Name = "Hajro", MiddleName = null, LastName = "Ulqinaku" },
        new Author { Id = Guid.NewGuid(), Name = "Giulio", MiddleName = null, LastName = "Variboba" },
        new Author { Id = Guid.NewGuid(), Name = "Ardian", MiddleName = null, LastName = "Vehbiu" },
        new Author { Id = Guid.NewGuid(), Name = "Naum", MiddleName = null, LastName = "Veqilharxhi" },
        new Author { Id = Guid.NewGuid(), Name = "Eqrem", MiddleName = "Bej", LastName = "Vlora" },
        new Author { Id = Guid.NewGuid(), Name = "Ornela", MiddleName = null, LastName = "Vorpsi" },
        new Author { Id = Guid.NewGuid(), Name = "Anila", MiddleName = null, LastName = "Wilms" },
        new Author { Id = Guid.NewGuid(), Name = "Jakov", MiddleName = null, LastName = "Xoxa" },
        new Author { Id = Guid.NewGuid(), Name = "Bilal", MiddleName = null, LastName = "Xhaferri" },
        new Author { Id = Guid.NewGuid(), Name = "Dhimitër", MiddleName = null, LastName = "Xhuvani" },
        new Author { Id = Guid.NewGuid(), Name = "Muçi", MiddleName = null, LastName = "Zade" },
        new Author { Id = Guid.NewGuid(), Name = "Injac", MiddleName = null, LastName = "Zamputi" },
        new Author { Id = Guid.NewGuid(), Name = "Tajar", MiddleName = null, LastName = "Zavalani" },
        new Author { Id = Guid.NewGuid(), Name = "Petraq", MiddleName = null, LastName = "Zoto" },
        new Author { Id = Guid.NewGuid(), Name = "Gjergj", MiddleName = null, LastName = "Zheji" },
        new Author { Id = Guid.NewGuid(), Name = "Petro", MiddleName = null, LastName = "Zheji" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedFrenchWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Victor", MiddleName = null, LastName = "Hugo" },
        new Author { Id = Guid.NewGuid(), Name = "Alexandre", MiddleName = null, LastName = "Dumas" },
        new Author { Id = Guid.NewGuid(), Name = "Gustave", MiddleName = null, LastName = "Flaubert" },
        new Author { Id = Guid.NewGuid(), Name = "Marcel", MiddleName = null, LastName = "Proust" },
        new Author { Id = Guid.NewGuid(), Name = "Émile", MiddleName = null, LastName = "Zola" },
        new Author { Id = Guid.NewGuid(), Name = "Honoré de", MiddleName = null, LastName = "Balzac" },
        new Author { Id = Guid.NewGuid(), Name = "Molière", MiddleName = null, LastName = null },
        new Author { Id = Guid.NewGuid(), Name = "Albert", MiddleName = null, LastName = "Camus" },
        new Author { Id = Guid.NewGuid(), Name = "Jean-Paul", MiddleName = null, LastName = "Sartre" },
        new Author { Id = Guid.NewGuid(), Name = "François-René de", MiddleName = null, LastName = "Chateaubriand" },
        new Author { Id = Guid.NewGuid(), Name = "André", MiddleName = null, LastName = "Gide" },
        new Author { Id = Guid.NewGuid(), Name = "Simone de", MiddleName = null, LastName = "Beauvoir" },
        new Author { Id = Guid.NewGuid(), Name = "Voltaire", MiddleName = null, LastName = null },
        new Author { Id = Guid.NewGuid(), Name = "Gérard de", MiddleName = null, LastName = "Nerval" },
        new Author { Id = Guid.NewGuid(), Name = "Stendhal", MiddleName = null, LastName = null },
        new Author { Id = Guid.NewGuid(), Name = "Jules", MiddleName = null, LastName = "Verne" },
        new Author { Id = Guid.NewGuid(), Name = "Colette", MiddleName = null, LastName = null },
        new Author { Id = Guid.NewGuid(), Name = "Jean", MiddleName = null, LastName = "Giono" },
        new Author { Id = Guid.NewGuid(), Name = "Louis-Ferdinand", MiddleName = null, LastName = "Céline" },
        new Author { Id = Guid.NewGuid(), Name = "Romain", MiddleName = null, LastName = "Rolland" },
        new Author { Id = Guid.NewGuid(), Name = "Guy de", MiddleName = null, LastName = "Maupassant" },
        new Author { Id = Guid.NewGuid(), Name = "Alfred de", MiddleName = null, LastName = "Musset" },
        new Author { Id = Guid.NewGuid(), Name = "Paul", MiddleName = null, LastName = "Valéry" },
        new Author { Id = Guid.NewGuid(), Name = "Michel", MiddleName = null, LastName = "Houellebecq" },
        new Author { Id = Guid.NewGuid(), Name = "Joris-Karl", MiddleName = null, LastName = "Huysmans" },
        new Author { Id = Guid.NewGuid(), Name = "Paul", MiddleName = null, LastName = "Verlaine" },
        new Author { Id = Guid.NewGuid(), Name = "Arthur", MiddleName = null, LastName = "Rimbaud" },
        new Author { Id = Guid.NewGuid(), Name = "Charles", MiddleName = null, LastName = "Baudelaire" },
        new Author { Id = Guid.NewGuid(), Name = "Marquis de", MiddleName = null, LastName = "Sade" },
        new Author { Id = Guid.NewGuid(), Name = "Georges", MiddleName = null, LastName = "Simenon" },
        new Author { Id = Guid.NewGuid(), Name = "Jacques", MiddleName = null, LastName = "Prévert" },
        new Author { Id = Guid.NewGuid(), Name = "René", MiddleName = null, LastName = "Descartes" },
        new Author { Id = Guid.NewGuid(), Name = "Blaise", MiddleName = null, LastName = "Pascal" },
        new Author { Id = Guid.NewGuid(), Name = "Henri", MiddleName = null, LastName = "Bergson" },
        new Author { Id = Guid.NewGuid(), Name = "Antoine de", MiddleName = null, LastName = "Saint-Exupéry" },
        new Author { Id = Guid.NewGuid(), Name = "Marguerite", MiddleName = null, LastName = "Duras" },
        new Author { Id = Guid.NewGuid(), Name = "Vladimir", MiddleName = null, LastName = "Nabokov" },
        new Author { Id = Guid.NewGuid(), Name = "Michel de", MiddleName = null, LastName = "Montaigne" },
        new Author { Id = Guid.NewGuid(), Name = "Marie de", MiddleName = null, LastName = "France" },
        new Author { Id = Guid.NewGuid(), Name = "Pierre", MiddleName = null, LastName = "Corneille" },
        new Author { Id = Guid.NewGuid(), Name = "François de", MiddleName = null, LastName = "La Rochefoucauld" },
        new Author { Id = Guid.NewGuid(), Name = "Choderlos de", MiddleName = null, LastName = "Laclos" },
        new Author { Id = Guid.NewGuid(), Name = "Jean de", MiddleName = null, LastName = "La Fontaine" },
        new Author { Id = Guid.NewGuid(), Name = "René", MiddleName = null, LastName = "Char" },
        new Author { Id = Guid.NewGuid(), Name = "Philippe", MiddleName = null, LastName = "Djian" },
        new Author { Id = Guid.NewGuid(), Name = "Patrick", MiddleName = null, LastName = "Modiano" },
        new Author { Id = Guid.NewGuid(), Name = "Jean", MiddleName = null, LastName = "Cocteau" },
        new Author { Id = Guid.NewGuid(), Name = "Yves", MiddleName = null, LastName = "Bonnefoy" },
        new Author { Id = Guid.NewGuid(), Name = "Rainer Maria", MiddleName = null, LastName = "Rilke" },
        new Author { Id = Guid.NewGuid(), Name = "Frédéric", MiddleName = null, LastName = "Beigbeder" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedBritishWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "William", MiddleName = null, LastName = "Shakespeare" },
        new Author { Id = Guid.NewGuid(), Name = "Charles", MiddleName = null, LastName = "Dickens" },
        new Author { Id = Guid.NewGuid(), Name = "George", MiddleName = null, LastName = "Orwell" },
        new Author { Id = Guid.NewGuid(), Name = "Jane", MiddleName = null, LastName = "Austen" },
        new Author { Id = Guid.NewGuid(), Name = "J.R.R.", MiddleName = null, LastName = "Tolkien" },
        new Author { Id = Guid.NewGuid(), Name = "Virginia", MiddleName = null, LastName = "Woolf" },
        new Author { Id = Guid.NewGuid(), Name = "Emily", MiddleName = null, LastName = "Brontë" },
        new Author { Id = Guid.NewGuid(), Name = "Charlotte", MiddleName = null, LastName = "Brontë" },
        new Author { Id = Guid.NewGuid(), Name = "Anne", MiddleName = null, LastName = "Brontë" },
        new Author { Id = Guid.NewGuid(), Name = "Agatha", MiddleName = null, LastName = "Christie" },
        new Author { Id = Guid.NewGuid(), Name = "Mark", MiddleName = null, LastName = "Twain" },
        new Author { Id = Guid.NewGuid(), Name = "Oscar", MiddleName = null, LastName = "Wilde" },
        new Author { Id = Guid.NewGuid(), Name = "Thomas", MiddleName = null, LastName = "Hardy" },
        new Author { Id = Guid.NewGuid(), Name = "John", MiddleName = null, LastName = "Milton" },
        new Author { Id = Guid.NewGuid(), Name = "D.H.", MiddleName = null, LastName = "Lawrence" },
        new Author { Id = Guid.NewGuid(), Name = "W.B.", MiddleName = null, LastName = "Yeats" },
        new Author { Id = Guid.NewGuid(), Name = "George", MiddleName = null, LastName = "Eliot" },
        new Author { Id = Guid.NewGuid(), Name = "Samuel", MiddleName = null, LastName = "Johnson" },
        new Author { Id = Guid.NewGuid(), Name = "H.G.", MiddleName = null, LastName = "Wells" },
        new Author { Id = Guid.NewGuid(), Name = "Ian", MiddleName = null, LastName = "Fleming" },
        new Author { Id = Guid.NewGuid(), Name = "J.K.", MiddleName = null, LastName = "Rowling" },
        new Author { Id = Guid.NewGuid(), Name = "Philip", MiddleName = null, LastName = "Pullman" },
        new Author { Id = Guid.NewGuid(), Name = "Arthur", MiddleName = null, LastName = "Conan Doyle" },
        new Author { Id = Guid.NewGuid(), Name = "Joseph", MiddleName = null, LastName = "Conrad" },
        new Author { Id = Guid.NewGuid(), Name = "Edgar Allan", MiddleName = null, LastName = "Poe" },
        new Author { Id = Guid.NewGuid(), Name = "Katherine", MiddleName = null, LastName = "Mansfield" },
        new Author { Id = Guid.NewGuid(), Name = "Alfred Lord", MiddleName = null, LastName = "Tennyson" },
        new Author { Id = Guid.NewGuid(), Name = "C.S.", MiddleName = null, LastName = "Lewis" },
        new Author { Id = Guid.NewGuid(), Name = "T.S.", MiddleName = null, LastName = "Eliot" },
        new Author { Id = Guid.NewGuid(), Name = "William", MiddleName = null, LastName = "Blake" },
        new Author { Id = Guid.NewGuid(), Name = "E.M.", MiddleName = null, LastName = "Forster" },
        new Author { Id = Guid.NewGuid(), Name = "Aldous", MiddleName = null, LastName = "Huxley" },
        new Author { Id = Guid.NewGuid(), Name = "Kingsley", MiddleName = null, LastName = "Amis" },
        new Author { Id = Guid.NewGuid(), Name = "V.S.", MiddleName = null, LastName = "Naipaul" },
        new Author { Id = Guid.NewGuid(), Name = "Jean", MiddleName = null, LastName = "Rhy" },
        new Author { Id = Guid.NewGuid(), Name = "Rebecca", MiddleName = null, LastName = "West" },
        new Author { Id = Guid.NewGuid(), Name = "Nigel", MiddleName = null, LastName = "Baldwin" },
        new Author { Id = Guid.NewGuid(), Name = "William", MiddleName = null, LastName = "Golding" },
        new Author { Id = Guid.NewGuid(), Name = "John", MiddleName = null, LastName = "Steinbeck" },
        new Author { Id = Guid.NewGuid(), Name = "J.R.R.", MiddleName = null, LastName = "Tolkien" },
        new Author { Id = Guid.NewGuid(), Name = "Virginia", MiddleName = null, LastName = "Woolf" },
        new Author { Id = Guid.NewGuid(), Name = "James", MiddleName = null, LastName = "Joyce" },
        new Author { Id = Guid.NewGuid(), Name = "Raymond", MiddleName = null, LastName = "Carver" },
        new Author { Id = Guid.NewGuid(), Name = "Boris", MiddleName = null, LastName = "Pasternak" },
        new Author { Id = Guid.NewGuid(), Name = "Bernard", MiddleName = null, LastName = "Shaw" },
        new Author { Id = Guid.NewGuid(), Name = "John", MiddleName = null, LastName = "Donne" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedAmericanWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Mark", MiddleName = null, LastName = "Twain" },
        new Author { Id = Guid.NewGuid(), Name = "Ernest", MiddleName = null, LastName = "Hemingway" },
        new Author { Id = Guid.NewGuid(), Name = "F. Scott", MiddleName = null, LastName = "Fitzgerald" },
        new Author { Id = Guid.NewGuid(), Name = "Harper", MiddleName = null, LastName = "Lee" },
        new Author { Id = Guid.NewGuid(), Name = "Toni", MiddleName = null, LastName = "Morrison" },
        new Author { Id = Guid.NewGuid(), Name = "John", MiddleName = null, LastName = "Steinbeck" },
        new Author { Id = Guid.NewGuid(), Name = "Walt", MiddleName = null, LastName = "Whitman" },
        new Author { Id = Guid.NewGuid(), Name = "Emily", MiddleName = null, LastName = "Dickinson" },
        new Author { Id = Guid.NewGuid(), Name = "Nathaniel", MiddleName = null, LastName = "Hawthorne" },
        new Author { Id = Guid.NewGuid(), Name = "Herman", MiddleName = null, LastName = "Melville" },
        new Author { Id = Guid.NewGuid(), Name = "Langston", MiddleName = null, LastName = "Hughes" },
        new Author { Id = Guid.NewGuid(), Name = "William", MiddleName = null, LastName = "Faulkner" },
        new Author { Id = Guid.NewGuid(), Name = "Maya", MiddleName = null, LastName = "Angelou" },
        new Author { Id = Guid.NewGuid(), Name = "Raymond", MiddleName = null, LastName = "Carver" },
        new Author { Id = Guid.NewGuid(), Name = "J.", MiddleName = "D.", LastName = "Salinger" },
        new Author { Id = Guid.NewGuid(), Name = "Kurt", MiddleName = null, LastName = "Vonnegut" },
        new Author { Id = Guid.NewGuid(), Name = "Sylvia", MiddleName = null, LastName = "Plath" },
        new Author { Id = Guid.NewGuid(), Name = "Ralph", MiddleName = null, LastName = "Waldo Emerson" },
        new Author { Id = Guid.NewGuid(), Name = "Jack", MiddleName = null, LastName = "Kerouac" },
        new Author { Id = Guid.NewGuid(), Name = "Alice", MiddleName = null, LastName = "Walker" },
        new Author { Id = Guid.NewGuid(), Name = "J.", MiddleName = "R.R.", LastName = "Tolkien" },
        new Author { Id = Guid.NewGuid(), Name = "Truman", MiddleName = null, LastName = "Capote" },
        new Author { Id = Guid.NewGuid(), Name = "Isaac", MiddleName = null, LastName = "Asimov" },
        new Author { Id = Guid.NewGuid(), Name = "Philip", MiddleName = null, LastName = "K. Dick" },
        new Author { Id = Guid.NewGuid(), Name = "Joyce", MiddleName = null, LastName = "Carol Oates" },
        new Author { Id = Guid.NewGuid(), Name = "John", MiddleName = null, LastName = "Updike" },
        new Author { Id = Guid.NewGuid(), Name = "Tennessee", MiddleName = null, LastName = "Williams" },
        new Author { Id = Guid.NewGuid(), Name = "Virginia", MiddleName = null, LastName = "Woolf" },
        new Author { Id = Guid.NewGuid(), Name = "David", MiddleName = null, LastName = "Foster Wallace" },
        new Author { Id = Guid.NewGuid(), Name = "Marlon", MiddleName = null, LastName = "James" },
        new Author { Id = Guid.NewGuid(), Name = "Zora", MiddleName = null, LastName = "Neale Hurston" },
        new Author { Id = Guid.NewGuid(), Name = "William", MiddleName = null, LastName = "Stryon" },
        new Author { Id = Guid.NewGuid(), Name = "Cheryl", MiddleName = null, LastName = "Strayed" },
        new Author { Id = Guid.NewGuid(), Name = "Thomas", MiddleName = null, LastName = "Pynchon" },
        new Author { Id = Guid.NewGuid(), Name = "Sandra", MiddleName = null, LastName = "Cisneros" },
        new Author { Id = Guid.NewGuid(), Name = "Don", MiddleName = null, LastName = "Delillo" },
        new Author { Id = Guid.NewGuid(), Name = "Margaret", MiddleName = null, LastName = "Atwood" },
        new Author { Id = Guid.NewGuid(), Name = "David", MiddleName = null, LastName = "Sedaris" },
        new Author { Id = Guid.NewGuid(), Name = "Chuck", MiddleName = null, LastName = "Palahniuk" },
        new Author { Id = Guid.NewGuid(), Name = "J.", MiddleName = "K.", LastName = "Rowling" },
        new Author { Id = Guid.NewGuid(), Name = "Neil", MiddleName = null, LastName = "Gaiman" },
        new Author { Id = Guid.NewGuid(), Name = "Stephen", MiddleName = null, LastName = "King" },
        new Author { Id = Guid.NewGuid(), Name = "Richard", MiddleName = null, LastName = "Yates" },
        new Author { Id = Guid.NewGuid(), Name = "Douglas", MiddleName = null, LastName = "Adams" },
        new Author { Id = Guid.NewGuid(), Name = "Alice", MiddleName = null, LastName = "Munro" },
        new Author { Id = Guid.NewGuid(), Name = "Bernard", MiddleName = null, LastName = "Malamud" },
        new Author { Id = Guid.NewGuid(), Name = "Michael", MiddleName = null, LastName = "Chabon" },
        new Author { Id = Guid.NewGuid(), Name = "Eudora", MiddleName = null, LastName = "Welty" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedPersianWriters(SQLiteAsyncConnection db)
    {
        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Rumi", MiddleName = null, LastName = "Jalal ad-Din" },
        new Author { Id = Guid.NewGuid(), Name = "Hafez", MiddleName = null, LastName = "Shirazi" },
        new Author { Id = Guid.NewGuid(), Name = "Omar", MiddleName = null, LastName = "Khayyam" },
        new Author { Id = Guid.NewGuid(), Name = "Saadi", MiddleName = null, LastName = "Shirazi" },
        new Author { Id = Guid.NewGuid(), Name = "Ferdowsi", MiddleName = null, LastName = "Hamedani" },
        new Author { Id = Guid.NewGuid(), Name = "Nizami", MiddleName = null, LastName = "Ganjavi" },
        new Author { Id = Guid.NewGuid(), Name = "Sadegh", MiddleName = null, LastName = "Hedayat" },
        new Author { Id = Guid.NewGuid(), Name = "Forugh", MiddleName = null, LastName = "Farrokhzad" },
        new Author { Id = Guid.NewGuid(), Name = "Ali", MiddleName = null, LastName = "Shariati" },
        new Author { Id = Guid.NewGuid(), Name = "Mohammad", MiddleName = "Ali", LastName = "Jamalzadeh" },
        new Author { Id = Guid.NewGuid(), Name = "Bahram", MiddleName = null, LastName = "Sadeghi" },
        new Author { Id = Guid.NewGuid(), Name = "Ahmad", MiddleName = null, LastName = "Shamlu" },
        new Author { Id = Guid.NewGuid(), Name = "Nima", MiddleName = null, LastName = "Yushij" },
        new Author { Id = Guid.NewGuid(), Name = "Shahram", MiddleName = null, LastName = "Azar" },
        new Author { Id = Guid.NewGuid(), Name = "Abdolkarim", MiddleName = null, LastName = "Soroush" },
        new Author { Id = Guid.NewGuid(), Name = "Faramarz", MiddleName = null, LastName = "Kobayashi" },
        new Author { Id = Guid.NewGuid(), Name = "Javad", MiddleName = null, LastName = "Ma’soumi" },
        new Author { Id = Guid.NewGuid(), Name = "Hassan", MiddleName = null, LastName = "Banafsheh" },
        new Author { Id = Guid.NewGuid(), Name = "Morteza", MiddleName = null, LastName = "Avini" },
        new Author { Id = Guid.NewGuid(), Name = "Mohammad Reza", MiddleName = null, LastName = "Shajarian" },
        new Author { Id = Guid.NewGuid(), Name = "Parvin", MiddleName = null, LastName = "E'tesami" },
        new Author { Id = Guid.NewGuid(), Name = "Sima", MiddleName = null, LastName = "Habibi" },
        new Author { Id = Guid.NewGuid(), Name = "Mohammad", MiddleName = null, LastName = "Ali Jamalzadeh" },
        new Author { Id = Guid.NewGuid(), Name = "Seyed", MiddleName = "Mahmood", LastName = "Tabatabai" },
        new Author { Id = Guid.NewGuid(), Name = "Taha", MiddleName = null, LastName = "Hosseini" },
        new Author { Id = Guid.NewGuid(), Name = "Mojgan", MiddleName = null, LastName = "Bayat" },
        new Author { Id = Guid.NewGuid(), Name = "Mehdi", MiddleName = null, LastName = "Aziznejad" },
        new Author { Id = Guid.NewGuid(), Name = "Mohammad", MiddleName = null, LastName = "Mahdavi-Damghani" },
        new Author { Id = Guid.NewGuid(), Name = "Abdolrahman", MiddleName = null, LastName = "Shahvar" },
        new Author { Id = Guid.NewGuid(), Name = "Gholamhossein", MiddleName = null, LastName = "Saedi" },
        new Author { Id = Guid.NewGuid(), Name = "Mahmood", MiddleName = null, LastName = "Davari" },
        new Author { Id = Guid.NewGuid(), Name = "Khosrow", MiddleName = null, LastName = "Shahriar" },
        new Author { Id = Guid.NewGuid(), Name = "Kaveh", MiddleName = null, LastName = "Golestan" },
        new Author { Id = Guid.NewGuid(), Name = "Shirin", MiddleName = null, LastName = "Ebadi" },
        new Author { Id = Guid.NewGuid(), Name = "Yadollah", MiddleName = null, LastName = "Sadeghi" },
        new Author { Id = Guid.NewGuid(), Name = "Marjane", MiddleName = null, LastName = "Satrapi" },
        new Author { Id = Guid.NewGuid(), Name = "Farid", MiddleName = null, LastName = "Hedayat" },
        new Author { Id = Guid.NewGuid(), Name = "Nader", MiddleName = null, LastName = "Naderi" },
        new Author { Id = Guid.NewGuid(), Name = "Kamal", MiddleName = null, LastName = "Maleki" },
        new Author { Id = Guid.NewGuid(), Name = "Mehdi", MiddleName = null, LastName = "Ahmadi" },
        new Author { Id = Guid.NewGuid(), Name = "Javad", MiddleName = null, LastName = "Yarshater" },
        new Author { Id = Guid.NewGuid(), Name = "Mirza", MiddleName = null, LastName = "Abdollah Khan" },
        new Author { Id = Guid.NewGuid(), Name = "Amir", MiddleName = null, LastName = "Khosrow" },
        new Author { Id = Guid.NewGuid(), Name = "Mohammad", MiddleName = null, LastName = "Reza" },
        new Author { Id = Guid.NewGuid(), Name = "Reza", MiddleName = null, LastName = "Badiyi" },
        new Author { Id = Guid.NewGuid(), Name = "Mehrdad", MiddleName = null, LastName = "Moeini" },
        new Author { Id = Guid.NewGuid(), Name = "Mahmood", MiddleName = null, LastName = "Kiani" },
        new Author { Id = Guid.NewGuid(), Name = "Hadi", MiddleName = null, LastName = "Seif" },
        new Author { Id = Guid.NewGuid(), Name = "Shahram", MiddleName = null, LastName = "Sadeghi" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedRussianWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Alexander", MiddleName = null, LastName = "Pushkin" },
        new Author { Id = Guid.NewGuid(), Name = "Leo", MiddleName = null, LastName = "Tolstoy" },
        new Author { Id = Guid.NewGuid(), Name = "Fyodor", MiddleName = null, LastName = "Dostoevsky" },
        new Author { Id = Guid.NewGuid(), Name = "Anton", MiddleName = null, LastName = "Chekhov" },
        new Author { Id = Guid.NewGuid(), Name = "Nikolai", MiddleName = null, LastName = "Gogol" },
        new Author { Id = Guid.NewGuid(), Name = "Ivan", MiddleName = null, LastName = "Turgenev" },
        new Author { Id = Guid.NewGuid(), Name = "Maxim", MiddleName = null, LastName = "Gorky" },
        new Author { Id = Guid.NewGuid(), Name = "Boris", MiddleName = null, LastName = "Pasternak" },
        new Author { Id = Guid.NewGuid(), Name = "Mikhail", MiddleName = null, LastName = "Bulgakov" },
        new Author { Id = Guid.NewGuid(), Name = "Alexander", MiddleName = null, LastName = "Solzhenitsyn" },
        new Author { Id = Guid.NewGuid(), Name = "Vladimir", MiddleName = null, LastName = "Nabokov" },
        new Author { Id = Guid.NewGuid(), Name = "Anton", MiddleName = null, LastName = "Chekhov" },
        new Author { Id = Guid.NewGuid(), Name = "Andrei", MiddleName = null, LastName = "Platonov" },
        new Author { Id = Guid.NewGuid(), Name = "Mikhail", MiddleName = null, LastName = "Sholokhov" },
        new Author { Id = Guid.NewGuid(), Name = "Vladimir", MiddleName = null, LastName = "Mayakovsky" },
        new Author { Id = Guid.NewGuid(), Name = "Yevgeny", MiddleName = null, LastName = "Zamyatin" },
        new Author { Id = Guid.NewGuid(), Name = "Isaac", MiddleName = null, LastName = "Babel" },
        new Author { Id = Guid.NewGuid(), Name = "Andrei", MiddleName = null, LastName = "Bely" },
        new Author { Id = Guid.NewGuid(), Name = "Vassily", MiddleName = null, LastName = "Grossman" },
        new Author { Id = Guid.NewGuid(), Name = "Dmitry", MiddleName = null, LastName = "Merezhkovsky" },
        new Author { Id = Guid.NewGuid(), Name = "Sergei", MiddleName = null, LastName = "Yesenin" },
        new Author { Id = Guid.NewGuid(), Name = "Anna", MiddleName = null, LastName = "Akhmatova" },
        new Author { Id = Guid.NewGuid(), Name = "Boris", MiddleName = null, LastName = "Grebenshchikov" },
        new Author { Id = Guid.NewGuid(), Name = "Vera", MiddleName = null, LastName = "Inber" },
        new Author { Id = Guid.NewGuid(), Name = "Aleksei", MiddleName = null, LastName = "Tolstoy" },
        new Author { Id = Guid.NewGuid(), Name = "Lev", MiddleName = null, LastName = "Gumilyov" },
        new Author { Id = Guid.NewGuid(), Name = "Vyacheslav", MiddleName = null, LastName = "Ivanov" },
        new Author { Id = Guid.NewGuid(), Name = "Yuri", MiddleName = null, LastName = "Dombrovsky" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedGermanWriters(SQLiteAsyncConnection db)
    {
        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Johann Wolfgang", MiddleName = null, LastName = "Goethe" },
        new Author { Id = Guid.NewGuid(), Name = "Friedrich", MiddleName = null, LastName = "Schiller" },
        new Author { Id = Guid.NewGuid(), Name = "Thomas", MiddleName = null, LastName = "Mann" },
        new Author { Id = Guid.NewGuid(), Name = "Franz", MiddleName = null, LastName = "Kafka" },
        new Author { Id = Guid.NewGuid(), Name = "Hermann", MiddleName = null, LastName = "Hesse" },
        new Author { Id = Guid.NewGuid(), Name = "Bertolt", MiddleName = null, LastName = "Brecht" },
        new Author { Id = Guid.NewGuid(), Name = "Günter", MiddleName = null, LastName = "Grass" },
        new Author { Id = Guid.NewGuid(), Name = "Heinrich", MiddleName = null, LastName = "Heine" },
        new Author { Id = Guid.NewGuid(), Name = "Rainer Maria", MiddleName = null, LastName = "Rilke" },
        new Author { Id = Guid.NewGuid(), Name = "Elias", MiddleName = null, LastName = "Canetti" },
        new Author { Id = Guid.NewGuid(), Name = "Stefan", MiddleName = null, LastName = "Zweig" },
        new Author { Id = Guid.NewGuid(), Name = "Christa", MiddleName = null, LastName = "Wolf" },
        new Author { Id = Guid.NewGuid(), Name = "Peter", MiddleName = null, LastName = "Handke" },
        new Author { Id = Guid.NewGuid(), Name = "Max", MiddleName = null, LastName = "Frisch" },
        new Author { Id = Guid.NewGuid(), Name = "Hermann", MiddleName = null, LastName = "Broch" },
        new Author { Id = Guid.NewGuid(), Name = "Klaus", MiddleName = null, LastName = "Mann" },
        new Author { Id = Guid.NewGuid(), Name = "Wolfgang", MiddleName = null, LastName = "Borchert" },
        new Author { Id = Guid.NewGuid(), Name = "Ingeborg", MiddleName = null, LastName = "Bachmann" },
        new Author { Id = Guid.NewGuid(), Name = "Günter", MiddleName = null, LastName = "Jauch" },
        new Author { Id = Guid.NewGuid(), Name = "Johann", MiddleName = null, LastName = "Gottfried Herder" },
        new Author { Id = Guid.NewGuid(), Name = "Georg Wilhelm Friedrich", MiddleName = null, LastName = "Hegel" },
        new Author { Id = Guid.NewGuid(), Name = "Friedrich", MiddleName = null, LastName = "Nietzsche" },
        new Author { Id = Guid.NewGuid(), Name = "Karl", MiddleName = null, LastName = "Marx" },
        new Author { Id = Guid.NewGuid(), Name = "Wilhelm", MiddleName = null, LastName = "von Humboldt" },
        new Author { Id = Guid.NewGuid(), Name = "Martin", MiddleName = null, LastName = "Luther" },
        new Author { Id = Guid.NewGuid(), Name = "Hannah", MiddleName = null, LastName = "Arendt" },
        new Author { Id = Guid.NewGuid(), Name = "Theodor", MiddleName = null, LastName = "Adorno" },
        new Author { Id = Guid.NewGuid(), Name = "Karl", MiddleName = null, LastName = "Jaspers" },
        new Author { Id = Guid.NewGuid(), Name = "Gottfried", MiddleName = null, LastName = "Wilhelm Leibniz" },
        new Author { Id = Guid.NewGuid(), Name = "Eckhart", MiddleName = null, LastName = "Tolle" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedItalianWriters(SQLiteAsyncConnection db)
    {
        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Dante", MiddleName = null, LastName = "Alighieri" },
        new Author { Id = Guid.NewGuid(), Name = "Giovanni", MiddleName = null, LastName = "Boccaccio" },
        new Author { Id = Guid.NewGuid(), Name = "Petrarca", MiddleName = null, LastName = "Francesco" },
        new Author { Id = Guid.NewGuid(), Name = "Italo", MiddleName = null, LastName = "Calvino" },
        new Author { Id = Guid.NewGuid(), Name = "Umberto", MiddleName = null, LastName = "Eco" },
        new Author { Id = Guid.NewGuid(), Name = "Alessandro", MiddleName = null, LastName = "Manzoni" },
        new Author { Id = Guid.NewGuid(), Name = "Giacomo", MiddleName = null, LastName = "Leopardi" },
        new Author { Id = Guid.NewGuid(), Name = "Luigi", MiddleName = null, LastName = "Pirandello" },
        new Author { Id = Guid.NewGuid(), Name = "Cesare", MiddleName = null, LastName = "Pavese" },
        new Author { Id = Guid.NewGuid(), Name = "Edoardo", MiddleName = null, LastName = "Sanguineti" },
        new Author { Id = Guid.NewGuid(), Name = "Elena", MiddleName = null, LastName = "Ferrante" },
        new Author { Id = Guid.NewGuid(), Name = "Niccolò", MiddleName = null, LastName = "Machiavelli" },
        new Author { Id = Guid.NewGuid(), Name = "Vincenzo", MiddleName = null, LastName = "Monti" },
        new Author { Id = Guid.NewGuid(), Name = "Carlo", MiddleName = null, LastName = "Collodi" },
        new Author { Id = Guid.NewGuid(), Name = "Mario", MiddleName = null, LastName = "Vargas Llosa" },
        new Author { Id = Guid.NewGuid(), Name = "Francesco", MiddleName = null, LastName = "Tasso" },
        new Author { Id = Guid.NewGuid(), Name = "Antonio", MiddleName = null, LastName = "Gramsci" },
        new Author { Id = Guid.NewGuid(), Name = "Giorgio", MiddleName = null, LastName = "Bassani" },
        new Author { Id = Guid.NewGuid(), Name = "Enrico", MiddleName = null, LastName = "Fermi" },
        new Author { Id = Guid.NewGuid(), Name = "Giuseppe", MiddleName = null, LastName = "Tomasi di Lampedusa" },
        new Author { Id = Guid.NewGuid(), Name = "Salvatore", MiddleName = null, LastName = "Quasimodo" },
        new Author { Id = Guid.NewGuid(), Name = "Giuseppe", MiddleName = null, LastName = "Verdi" },
        new Author { Id = Guid.NewGuid(), Name = "Carlo", MiddleName = null, LastName = "Emilio Gadda" },
        new Author { Id = Guid.NewGuid(), Name = "Gabriele", MiddleName = null, LastName = "D'Annunzio" },
        new Author { Id = Guid.NewGuid(), Name = "Cesare", MiddleName = null, LastName = "Beccaria" },
        new Author { Id = Guid.NewGuid(), Name = "Fabrizio", MiddleName = null, LastName = "De André" },
        new Author { Id = Guid.NewGuid(), Name = "Vittorio", MiddleName = null, LastName = "Alfieri" },
        new Author { Id = Guid.NewGuid(), Name = "Aldo", MiddleName = null, LastName = "Moro" },
        new Author { Id = Guid.NewGuid(), Name = "Pier Paolo", MiddleName = null, LastName = "Pasolini" },
        new Author { Id = Guid.NewGuid(), Name = "Tullio", MiddleName = null, LastName = "De Mauro" },
        new Author { Id = Guid.NewGuid(), Name = "Luciana", MiddleName = null, LastName = "Lombardo" },
        new Author { Id = Guid.NewGuid(), Name = "Antonio", MiddleName = null, LastName = "Negri" },
        new Author { Id = Guid.NewGuid(), Name = "Marco", MiddleName = null, LastName = "Bellocchio" },
        new Author { Id = Guid.NewGuid(), Name = "Giovanni", MiddleName = null, LastName = "Verga" },
        new Author { Id = Guid.NewGuid(), Name = "Matilde", MiddleName = null, LastName = "Serao" },
        new Author { Id = Guid.NewGuid(), Name = "Maria", MiddleName = null, LastName = "Montessori" },
        new Author { Id = Guid.NewGuid(), Name = "Federico", MiddleName = null, LastName = "Fellini" },
        new Author { Id = Guid.NewGuid(), Name = "Gabriele", MiddleName = null, LastName = "Bocaccini" },
        new Author { Id = Guid.NewGuid(), Name = "Dino", MiddleName = null, LastName = "Buzzati" },
        new Author { Id = Guid.NewGuid(), Name = "Piero", MiddleName = null, LastName = "Gobetti" },
        new Author { Id = Guid.NewGuid(), Name = "Antonio", MiddleName = null, LastName = "Gramsci" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedSpanishWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Miguel", MiddleName = null, LastName = "de Cervantes" },
        new Author { Id = Guid.NewGuid(), Name = "Gabriel", MiddleName = null, LastName = "García Márquez" },
        new Author { Id = Guid.NewGuid(), Name = "Federico", MiddleName = null, LastName = "García Lorca" },
        new Author { Id = Guid.NewGuid(), Name = "Juan", MiddleName = null, LastName = "Rulfo" },
        new Author { Id = Guid.NewGuid(), Name = "Carlos", MiddleName = null, LastName = "Ruiz Zafón" },
        new Author { Id = Guid.NewGuid(), Name = "Mario", MiddleName = null, LastName = "Vargas Llosa" },
        new Author { Id = Guid.NewGuid(), Name = "Pablo", MiddleName = null, LastName = "Neruda" },
        new Author { Id = Guid.NewGuid(), Name = "Isabel", MiddleName = null, LastName = "Allende" },
        new Author { Id = Guid.NewGuid(), Name = "Jorge", MiddleName = null, LastName = "Luis Borges" },
        new Author { Id = Guid.NewGuid(), Name = "Salvador", MiddleName = null, LastName = "Elizondo" },
        new Author { Id = Guid.NewGuid(), Name = "María", MiddleName = null, LastName = "Dueñas" },
        new Author { Id = Guid.NewGuid(), Name = "Luis", MiddleName = null, LastName = "Buñuel" },
        new Author { Id = Guid.NewGuid(), Name = "Arturo", MiddleName = null, LastName = "Pérez-Reverte" },
        new Author { Id = Guid.NewGuid(), Name = "Ana", MiddleName = null, LastName = "María Shua" },
        new Author { Id = Guid.NewGuid(), Name = "Ángeles", MiddleName = null, LastName = "Mastretta" },
        new Author { Id = Guid.NewGuid(), Name = "Eduardo", MiddleName = null, LastName = "Galeano" },
        new Author { Id = Guid.NewGuid(), Name = "Juan", MiddleName = null, LastName = "Benet" },
        new Author { Id = Guid.NewGuid(), Name = "Rafael", MiddleName = null, LastName = "Chimeno" },
        new Author { Id = Guid.NewGuid(), Name = "Antonio", MiddleName = null, LastName = "Muñoz Molina" },
        new Author { Id = Guid.NewGuid(), Name = "Almudena", MiddleName = null, LastName = "Grandes" },
        new Author { Id = Guid.NewGuid(), Name = "Clara", MiddleName = null, LastName = "Sánchez" },
        new Author { Id = Guid.NewGuid(), Name = "José", MiddleName = null, LastName = "Saramago" },
        new Author { Id = Guid.NewGuid(), Name = "Miguel Ángel", MiddleName = null, LastName = "Asturias" },
        new Author { Id = Guid.NewGuid(), Name = "Victor", MiddleName = null, LastName = "Hugo" },
        new Author { Id = Guid.NewGuid(), Name = "Leopoldo", MiddleName = null, LastName = "Marechal" },
        new Author { Id = Guid.NewGuid(), Name = "Blanca", MiddleName = null, LastName = "Varela" },
        new Author { Id = Guid.NewGuid(), Name = "Dolores", MiddleName = null, LastName = "O'Riordan" },
        new Author { Id = Guid.NewGuid(), Name = "Luis", MiddleName = null, LastName = "Cernuda" },
        new Author { Id = Guid.NewGuid(), Name = "José Luis", MiddleName = null, LastName = "Sampedro" },
        new Author { Id = Guid.NewGuid(), Name = "Manuel", MiddleName = null, LastName = "Pérez" },
        new Author { Id = Guid.NewGuid(), Name = "Héctor", MiddleName = null, LastName = "Pallares" },
        new Author { Id = Guid.NewGuid(), Name = "Lola", MiddleName = null, LastName = "Bello" },
        new Author { Id = Guid.NewGuid(), Name = "Antonio", MiddleName = null, LastName = "Gala" },
        new Author { Id = Guid.NewGuid(), Name = "Lorenzo", MiddleName = null, LastName = "Silva" },
        new Author { Id = Guid.NewGuid(), Name = "Joaquín", MiddleName = null, LastName = "Gallegos" },
        new Author { Id = Guid.NewGuid(), Name = "Concha", MiddleName = null, LastName = "Lladó" },
        new Author { Id = Guid.NewGuid(), Name = "José", MiddleName = null, LastName = "Donoso" },
        new Author { Id = Guid.NewGuid(), Name = "Carlos", MiddleName = null, LastName = "Fuentes" },
        new Author { Id = Guid.NewGuid(), Name = "Laura", MiddleName = null, LastName = "Esquivel" },
        new Author { Id = Guid.NewGuid(), Name = "Antonio", MiddleName = null, LastName = "Lobo Antunes" },
        new Author { Id = Guid.NewGuid(), Name = "Mario", MiddleName = null, LastName = "Levrero" },
        new Author { Id = Guid.NewGuid(), Name = "Guadalupe", MiddleName = null, LastName = "Nettel" },
        new Author { Id = Guid.NewGuid(), Name = "Ricardo", MiddleName = null, LastName = "Piglia" },
        new Author { Id = Guid.NewGuid(), Name = "Carlos", MiddleName = null, LastName = "Fuentes" },
        new Author { Id = Guid.NewGuid(), Name = "José", MiddleName = null, LastName = "Cardoso" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedJapaneseWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Haruki", MiddleName = null, LastName = "Murakami" },
        new Author { Id = Guid.NewGuid(), Name = "Yukio", MiddleName = null, LastName = "Mishima" },
        new Author { Id = Guid.NewGuid(), Name = "Natsume", MiddleName = null, LastName = "Sōseki" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedArabicWriters(SQLiteAsyncConnection db)
    {
        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Naguib", MiddleName = null, LastName = "Mahfouz" },
        new Author { Id = Guid.NewGuid(), Name = "Khalil", MiddleName = null, LastName = "Gibran" },
        new Author { Id = Guid.NewGuid(), Name = "Tayeb", MiddleName = null, LastName = "Salih" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedAfricanWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Chinua", MiddleName = null, LastName = "Achebe" },
        new Author { Id = Guid.NewGuid(), Name = "Wole", MiddleName = null, LastName = "Soyinka" },
        new Author { Id = Guid.NewGuid(), Name = "Ngũgĩ wa", MiddleName = null, LastName = "Thiong'o" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedIndianWriters(SQLiteAsyncConnection db)
    {


        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Rabindranath", MiddleName = null, LastName = "Tagore" },
        new Author { Id = Guid.NewGuid(), Name = "R.K.", MiddleName = null, LastName = "Narayan" },
        new Author { Id = Guid.NewGuid(), Name = "Arundhati", MiddleName = null, LastName = "Roy" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedTurkishWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Orhan", MiddleName = null, LastName = "Pamuk" },
        new Author { Id = Guid.NewGuid(), Name = "Nazım", MiddleName = null, LastName = "Hikmet" },
        new Author { Id = Guid.NewGuid(), Name = "Elif", MiddleName = null, LastName = "Shafak" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedLatinAmericanWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Mario", MiddleName = null, LastName = "Vargas Llosa" },
        new Author { Id = Guid.NewGuid(), Name = "Carlos", MiddleName = null, LastName = "Fuentes" },
        new Author { Id = Guid.NewGuid(), Name = "Julio", MiddleName = null, LastName = "Cortázar" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedDutchWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Anne", MiddleName = null, LastName = "Frank" },
        new Author { Id = Guid.NewGuid(), Name = "Harry", MiddleName = null, LastName = "Mulisch" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedScandinavianWriters(SQLiteAsyncConnection db)
    {
        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Henrik", MiddleName = null, LastName = "Ibsen" },
        new Author { Id = Guid.NewGuid(), Name = "August", MiddleName = null, LastName = "Strindberg" },
        new Author { Id = Guid.NewGuid(), Name = "Tove", MiddleName = null, LastName = "Jansson" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedHungarianWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Imre", MiddleName = null, LastName = "Kertész" },
        new Author { Id = Guid.NewGuid(), Name = "Sándor", MiddleName = null, LastName = "Márai" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedPolishWriters(SQLiteAsyncConnection db)
    {
        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Wisława", MiddleName = null, LastName = "Szymborska" },
        new Author { Id = Guid.NewGuid(), Name = "Czesław", MiddleName = null, LastName = "Miłosz" },
        new Author { Id = Guid.NewGuid(), Name = "Andrzej", MiddleName = null, LastName = "Sapkowski" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedChineseWriters(SQLiteAsyncConnection db)
    {

        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Lu", MiddleName = null, LastName = "Xun" },
        new Author { Id = Guid.NewGuid(), Name = "Mo", MiddleName = null, LastName = "Yan" },
        new Author { Id = Guid.NewGuid(), Name = "Amy", MiddleName = null, LastName = "Tan" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedCanadianWriters(SQLiteAsyncConnection db)
    {
        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Margaret", MiddleName = null, LastName = "Atwood" },
        new Author { Id = Guid.NewGuid(), Name = "Leonard", MiddleName = null, LastName = "Cohen" },
        new Author { Id = Guid.NewGuid(), Name = "Alice", MiddleName = null, LastName = "Munro" }
    };

        db.InsertAllAsync(authors).Wait();
    }
    public static void SeedGreekWriters(SQLiteAsyncConnection db)
    {
        var authors = new List<Author>
    {
        new Author { Id = Guid.NewGuid(), Name = "Nikos", MiddleName = null, LastName = "Kazantzakis" },
        new Author { Id = Guid.NewGuid(), Name = "Yannis", MiddleName = null, LastName = "Ritsos" },
        new Author { Id = Guid.NewGuid(), Name = "George", MiddleName = null, LastName = "Seferis" }
    };

        db.InsertAllAsync(authors).Wait();
    }
}
