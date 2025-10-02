using System;
class Program
{
    static void Main(string[] args)
    {
        Personaggio personaggio1 = new Guerriero() { Livello = 20, Vita = 200, Mana = 0, Amico = false};
        Personaggio personaggio2 = new Cacciatore() { Nome = "Rexxar", Livello = 20, Vita = 90, Mana = 10, Amico = true};
        Personaggio personaggio3 = new Sacerdote() { Nome = "Anduin", Livello = 20, Vita = 100, Mana = 40, Amico = true };
        try
        {
            personaggio1.StampaInfo();
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine("L'argomento è nullo");
        }
        personaggio1.Attacca(personaggio2);
        try
        {
            personaggio2.AbilitaSpeciale(personaggio1);
            personaggio3.AbilitaSpeciale(personaggio2);
            personaggio1.Attacca(personaggio3);
            personaggio2.AbilitaSpeciale(personaggio1);
            personaggio3.AbilitaSpeciale(personaggio1);
        }
        catch (ManaInsufficienteException ex)
        {
            Console.WriteLine("Errore mana:" + ex.Message);
        }
        catch (BersaglioNonValidoException ex) 
        {
            Console.WriteLine("Errore bersaglio:" + ex.Message);
        }
        Console.WriteLine("cià");

    }
}
public class Personaggio
{
    
    public string Nome {  get; set; }
    public int Livello { get; set; }
    public int Vita { get; set; }
    public int Mana { get; set; }
    public bool Amico { get; set; }
    public virtual void Attacca(Personaggio bersaglio) { }
    public virtual void AbilitaSpeciale(Personaggio bersaglio) { }
    public virtual void RiceviDanno(int danno) 
    {
        Vita -= danno;
    }
    public virtual bool Morto()
    {
        return Vita <= 0;
    }
    public void StampaInfo()
    {
        if (Nome == null) throw new ArgumentNullException();
        Console.WriteLine($"{Nome}");
    }
}
public class Guerriero : Personaggio
{
    public override void Attacca(Personaggio bersaglio)
    {
        if (bersaglio.Vita >= Livello * 3)
        {
            Vita -= Livello * 3;
        }
        else { Console.WriteLine($"{bersaglio.Nome} è morto cià"); }
    }
    public override void AbilitaSpeciale(Personaggio bersaglio)
    {
        if (bersaglio.Vita >= Livello * 6)
        {
            Vita -= Livello * 6;
        }
        else { Console.WriteLine($"{bersaglio.Nome} è morto cià"); }
    }
}
public class Cacciatore : Personaggio
{
    public override void Attacca(Personaggio bersaglio)
    {
        if (bersaglio.Vita >= Livello * 2)
        {
            Vita -= Livello * 2;
        }
        else { Console.WriteLine($"{bersaglio.Nome} è morto cià"); }
    }
    public override void AbilitaSpeciale(Personaggio bersaglio)
    {
        if  (Mana >= 10)
        {
            Mana -= 10;
            if (bersaglio.Vita >= Livello * 5)
            {
                bersaglio.Vita -= Livello * 5;
            }
            else { Console.WriteLine($"{bersaglio.Nome} è morto cià"); }
        }
        else throw new ManaInsufficienteException($"{Nome} non ha abbastanza mana per l'attacco speciale cià");
    }
}
public class Sacerdote : Personaggio
{
    public override void Attacca(Personaggio bersaglio)
    {
        if (bersaglio.Vita >= Livello * 1)
        {
            Vita -= Livello * 1;
        }
        else { Console.WriteLine($"{bersaglio.Nome} è morto cià"); }
    }
    public override void AbilitaSpeciale(Personaggio bersaglio)
    {
        if (Mana >= 15)
        {
            Mana -= 15;
            if (bersaglio.Amico)
            {
                bersaglio.Vita += Livello * 4;
            }
            else throw new BersaglioNonValidoException($" {bersaglio.Nome} non è un alleato cià");
        }
        else throw new ManaInsufficienteException($"  {Nome} non ha abbastanza mana per l'attacco speciale cià");
    }
}

public class ManaInsufficienteException : Exception
{
    public ManaInsufficienteException(string message) : base(message){ }
}
public class BersaglioNonValidoException : Exception
{
    public BersaglioNonValidoException(string message) : base(message){ }
}

//commento per Ricardo