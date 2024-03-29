﻿using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Random slump = new Random();
            Console.WriteLine("Välkomna till BlackJack, hur många vill spela?");
            int a = int.Parse(Console.ReadLine());
            int[] antal = new int[a];
            int b = 0;
            foreach (int e in antal)
            {
                antal[b] = b;
                b++;
            }
            int[] bets = new int[a];
            int[] bet1 = new int[a];
            int[] bet2 = new int[a];
            int[] bet3 = new int[a];
            int kortvärde = 0;
            foreach (int e in antal)
            {
                Console.WriteLine("Hur mycket vill spelare " + (e + 1) + " betta?");
                bets[e] = int.Parse(Console.ReadLine());
                bet1[e] = bets[e];
                bet2[e] = bets[e];
                bet3[e] = bets[e];
            }
            List<int[]> allabets = new List<int[]>();
            allabets.Add(bets);
            allabets.Add(bet1);
            allabets.Add(bet2);
            allabets.Add(bet3);
            int[] spelarkvärde = new int[a];
            int[] split1 = new int[a];
            int[] split2 = new int[a];
            int[] split3 = new int[a];
            List<int[]> kvärden = new List<int[]>();
            kvärden.Add(spelarkvärde);
            kvärden.Add(split1);
            kvärden.Add(split2);
            kvärden.Add(split3);
            string[] allakort = new string[52];
            string[] datornsallakort = new string[10];
            int i = 0;
            int c = 0;
            int dantaläss = 0;
            int datorkvärde = 0;
            int[] skräplista = new int[12];
            string datornskort = Kortutdelare(slump, ref allakort, ref dantaläss, ref i, ref datornsallakort, ref c, ref datorkvärde, ref kortvärde, ref skräplista);
            Console.WriteLine("Datorn har kortet " + datornskort + " med värde " + datorkvärde + ".");
            string spelarenskort;
            b = 0;
            foreach (int e in antal)
            {
                string spelarnamn = "Spelare " + (e + 1);
                Console.WriteLine("");
                bool split = false;

                int p = 0;
                int antaläss = 0;
                string[] spelarensallakort = new string[12];
                bool kanskesplit = false;
                int[] splittest = new int[12];
                for (int n = 0; n != 2; n++)
                {
                    spelarenskort = Kortutdelare(slump, ref allakort, ref antaläss, ref i, ref spelarensallakort, ref p, ref spelarkvärde[e], ref kortvärde, ref splittest);
                }
                if (splittest[0] == splittest[1])
                {
                    kanskesplit = true;
                    splittest[1] = 0;
                    splittest[0] = 0;
                }
                bool y = false;
                Console.WriteLine(spelarnamn + ", du har korten " + Skaskrivas(spelarensallakort, p) + " med totalt värde " + spelarkvärde[e] + ".");
                if (spelarkvärde[e] == 21)
                {
                    Console.WriteLine("Du fick BlackJack, grattis! Du vann " + (bets[e] * 1.5) + " kr.");
                    y = true;
                    spelarkvärde[e] = 0;
                }
                int splitnr = 0;
                Blackjack(e, a , ref y, antaläss, ref spelarkvärde[e], ref bets[e], spelarnamn, ref split, slump, ref kanskesplit, ref allakort, ref i, ref spelarensallakort, ref p, ref skräplista, ref kortvärde, ref allabets, ref kvärden, ref splitnr, ref splittest);
            }
            while (datorkvärde < 17)
            {
                Kortutdelare(slump, ref allakort, ref dantaläss, ref i, ref datornsallakort, ref c, ref datorkvärde, ref kortvärde, ref skräplista);
                if (datorkvärde > 21 && dantaläss > 0)
                {
                    datorkvärde -= 10;
                    dantaläss--;
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Datorn fick korten " + Skaskrivas(datornsallakort, c) + " av värde " + datorkvärde + ".");
            if (datorkvärde > 21)
            {
                Console.WriteLine("Då datorn fick över 21 förlorar den, alla som fortfarande är kvar vinner.");
                foreach (int e in antal)
                {
                    if (spelarkvärde[e] != 0)
                    {
                        Console.WriteLine("Spelare " + (e + 1) + ", grattis du vinner " + bets[e] + "kr!");
                    }
                }
                Console.WriteLine("Spelet är slut, starta om programmet för attt köra igen.");
                return;
            }
            else
            {
                foreach (int e in antal)
                {
                    if (spelarkvärde[e] != 0)
                    {
                        Console.WriteLine("");
                        if (spelarkvärde[e] > datorkvärde)
                        {
                            Console.WriteLine("Spelare " + (e + 1) + " du fick kortvärdet " + spelarkvärde[e] + " vilket är mer än datorns kortvärde på " + datorkvärde + ". Grattis, du vinner " + bets[e] + "kr!");
                        }
                        else if (spelarkvärde[e] < datorkvärde)
                        {
                            Console.WriteLine("Spelare " + (e + 1) + " du fick kortvärdet " + spelarkvärde[e] + " vilket är mindre än datorns kortvärde på " + datorkvärde + ". Du förlorar " + bets[e] + "kr.");
                        }
                        else if (spelarkvärde[e] == datorkvärde)
                        {
                            Console.WriteLine("Spelare " + (e + 1) + " du fick kortvärdet " + spelarkvärde[e] + " vilket är lika mycket som datorns kortvärde på " + datorkvärde + ". Du får pengarna tillbaka");
                        }
                        else
                        {
                            Console.WriteLine("ERROR");
                        }
                    }
                }
                Console.WriteLine("Spelet är slut, starta om programmet för attt köra igen.");
                return;
            }
        }
        static int Kortgivare(Random slump)
        {
            return (slump.Next(1, 14));
        }
        static void Split(ref string spelarnamn, ref int splitnr, int e, ref List<int[]> allabets, ref List<int[]> kvärden, int a, ref int antaläss, ref bool split, Random slump, ref bool kanskesplit, ref string[] allakort, ref int i, ref int p, ref int[] skräplista, ref string[] kortlista, ref bool y, ref int kortvärde, ref int[] splittest)
        {
            spelarnamn = "Spelare " + e + " hand " + (splitnr + 1);
            kvärden[splitnr][e] /= 2;
            int[] nysplittest = new int[12];
            Splittestreset(ref splittest, ref nysplittest, p);
            p = 1;
            string[] nykortlista = new string[a];
            nykortlista[0] = kortlista[0];
            if (antaläss > 2)
            {
                kvärden[splitnr][e] += 10;
            }
            kvärden[splitnr + 1][e] = kvärden[splitnr][e];
            antaläss /= 2;
            int nyantaläss = antaläss;
            Blackjack(e, a, ref y, antaläss, ref kvärden[splitnr][e], ref allabets[splitnr][e], spelarnamn, ref split, slump, ref kanskesplit, ref allakort, ref i, ref nykortlista, ref p, ref skräplista, ref kortvärde, ref allabets, ref kvärden, ref splitnr, ref splittest);
            string[] ännunyarekortlista = new string[a];
            ännunyarekortlista[0] = kortlista[1];
            splitnr++;
            p = 1;
            spelarnamn = "Spelare " + e + " hand " + (splitnr + 1);
            Blackjack(e, a, ref y, nyantaläss, ref kvärden[splitnr][e], ref allabets[splitnr][e], spelarnamn, ref split, slump, ref kanskesplit, ref allakort, ref i, ref ännunyarekortlista, ref p, ref skräplista, ref kortvärde, ref allabets, ref kvärden, ref splitnr, ref splittest);
        }
        static void Splittestreset(ref int[] splittest, ref int[] nsplittest, int p)
        {
            int n = 1;
            int f = 0;
            while (true)
            {
                int b = 1;
                if (n == 0)
                {
                    break;
                }
                n = 0;
                for (int i = 1; i < 12; i++)
                {
                    Jämförare(ref splittest[b - 1], ref splittest[b], ref n, ref f);
                    b++;
                }
            }
            int k = 1;
            nsplittest[0] = splittest[0];
            if (p > 2)
            {
                splittest[1] = splittest[2];
                nsplittest[1] = splittest[1];
                k++;
                if (p > 4)
                {
                    splittest[2] = splittest[4];
                    nsplittest[2] = splittest[2];
                    k++;
                    if (p > 6)
                    {
                        splittest[3] = splittest[6];
                        nsplittest[3] = splittest[3];
                        k++;
                        if (p > 8)
                        {
                            splittest[4] = splittest[8];
                            nsplittest[4] = splittest[4];
                            k++;
                            if (p > 10)
                            {
                                splittest[5] = splittest[10];
                                nsplittest[5] = splittest[5];
                                k++;
                            }
                        }
                    }
                }
            }
            for (int i = 11; i > k; i--)
            {
                splittest[i] = 0;
            }
        }
        static void Jämförare(ref int a, ref int b, ref int n, ref int f)
        {
            if (a > b && b != 0)
            {
                int v = a;
                a = b;
                b = v;
                n++;
                f++;
            }
        }
        static string Kort(int kortvärde, Random slump, ref string kortnamn)
        {
            string kortvalör;
            int kortvalörsvärde = slump.Next(1, 5);
            if (kortvalörsvärde == 4)
            {
                kortvalör = "Hjärter";
            }
            else if (kortvalörsvärde == 3)
            {
                kortvalör = "Spader";
            }
            else if (kortvalörsvärde == 2)
            {
                kortvalör = "Ruter";
            }
            else if (kortvalörsvärde == 1)
            {
                kortvalör = "Klöver";
            }
            else
            {
                return "error";
            }
            if (kortvärde == 11)
            {
                kortnamn = "Knekt";
            }
            else if (kortvärde == 12)
            {
                kortnamn = "Dam";
            }
            else if (kortvärde == 13)
            {
                kortnamn = "Kung";
            }
            else if (kortvärde == 1)
            {
                kortnamn = "Äss";
            }
            else
            {
                kortnamn = Convert.ToString(kortvärde);
            }
            return (kortvalör + " " + kortnamn);
        }
        static int Kortvärde(int kortvärde)
        {
            if (kortvärde >= 11)
            {
                kortvärde = 10;
            }
            else if (kortvärde == 1)
            {
                kortvärde = 11;
            }
            return kortvärde;
        }
        static void Blackjack(int e, int a, ref bool y, int antaläss, ref int kvärde, ref int bet, string spelarnamn, ref bool split, Random slump, ref bool kanskesplit, ref string[] allakort, ref int i, ref string[] spelarensallakort, ref int p, ref int[] skräplista, ref int kortvärde, ref List<int[]> allabets, ref List<int[]> kvärden, ref int splitnr, ref int[] splittest)

        {
            string spelarenskort;
            while (y == false)
            {
                if (kvärde > 21)
                {
                    if (antaläss == 2 && p == 2)
                    {
                        Console.WriteLine("Du har 2 äss, vill du splitta eller minska värdet på en av dem?");
                        string asvar = Console.ReadLine();
                        if (asvar == "Split" || asvar == "split" || asvar == "sp")
                        {
                            Console.WriteLine("Ok, delar dina kort på två händer.");
                            Console.WriteLine("Din första hand har kortet " + spelarensallakort[0] + " och din andra hand kortet " + spelarensallakort[1] + ", båda har värdet " + (kvärde / 2) + ".");
                            Split(ref spelarnamn, ref splitnr, e, ref allabets, ref kvärden, a, ref antaläss, ref split, slump, ref kanskesplit, ref allakort, ref i, ref p, ref skräplista, ref spelarensallakort, ref y, ref kortvärde, ref splittest);
                            return;
                        }
                    }
                    if (antaläss > 0)
                    {
                        kvärde -= 10;
                        Console.WriteLine("Du fick över 21, du hade dock " + antaläss + " äss så ett av dem byter värde till 1.");
                        antaläss--;
                        Console.WriteLine("Du har nu kortvärdet " + kvärde + ".");
                    }
                    else
                    {
                        Console.WriteLine("Du fick över 21 och åker ut ur spelet. Du förlorar " + bet + "kr.");
                        kvärde = 0;
                        break;
                    }
                }
                kanskesplit = Splittest();
                if (kanskesplit == true)
                {
                    Console.Write("Split, ");
                }
                Console.WriteLine("Hit, Stand eller Double?");
                string svar = Console.ReadLine();
                if (svar == "Double" || svar == "double" || svar == "d")
                {
                    bet *= 2;
                    string[] kortlista = new string[2];
                    for (int n = 0; n != 2; n++)
                    {
                        spelarenskort = Kortutdelare(slump, ref allakort, ref antaläss, ref i, ref spelarensallakort, ref p, ref kvärde, ref kortvärde, ref skräplista);
                        kortlista[n] = spelarenskort;
                    }
                    int u = 0;
                    Console.WriteLine("Du fick du korten " + Skaskrivas(kortlista, 2) + ".");
                    Console.WriteLine("Ditt totala kortvärde är nu " + kvärde + ".");
                    while (kvärde > 21)
                    {
                        if (antaläss > 0)
                        {
                            kvärde -= 10;
                            u++;
                            antaläss--;
                        }
                        else
                        {
                            Console.WriteLine("Du fick över 21 och åker ut ur spelet. Du förlorar " + bet + " kr.");
                            kvärde = 0;
                            if (u > 0)
                            {
                                Console.WriteLine("Du hade äss men de räckte inte för att rädda dig.");
                            }
                            break;
                        }
                    }
                    if (u > 0 && kvärde != 0)
                    {
                        Console.WriteLine("Ditt kortvärde var över 21. Du hade dock " + (u + antaläss) + " äss så " + u + " av dem byter värde till 1.");
                        Console.WriteLine(spelarnamn + ", ditt slutgiltliga kortvärde är " + kvärde + " med korten " + Skaskrivas(spelarensallakort, p) + ".");
                    }
                    else if (kvärde != 0)
                    {
                        Console.WriteLine(spelarnamn + ", du slutade på korten " + Skaskrivas(spelarensallakort, p) + " med värdet " + kvärde + ".");
                    }
                    y = true;
                }
                else if (svar == "Hit" || svar == "hit" || svar == "h")
                {
                    spelarenskort = Kortutdelare(slump, ref allakort, ref antaläss, ref i, ref spelarensallakort, ref p, ref kvärde, ref kortvärde, ref skräplista);
                    Console.WriteLine("Du fick kortet " + spelarenskort + " av värde " + kortvärde + ", totalt har du nu kortvärdet " + kvärde + ".");
                    if (kvärde == 21)
                    {
                        Console.WriteLine("Grattis, du fick 21 med korten " + Skaskrivas(spelarensallakort, p) + " och har gått vidare.");
                    }
                }
                else if (svar == "Stand" || svar == "stand" || svar == "s")
                {
                    Console.WriteLine("Spelare " + (e + 1) + ", du slutade på korten " + Skaskrivas(spelarensallakort, p) + " med värde " + kvärde);
                    break;
                }
                else if ((svar == "Split" || svar == "split" || svar == "sp") && kanskesplit == true)
                {
                    Console.WriteLine("Ok, delar dina kort på två händer.");
                    Console.WriteLine("Din första hand har kortet " + spelarensallakort[0] + " och din andra hand kortet " + spelarensallakort[1] + ", båda har värdet " + (kvärde / 2) + ".");
                    Split(ref spelarnamn, ref splitnr, e, ref allabets, ref kvärden, a, ref antaläss, ref split, slump, ref kanskesplit, ref allakort, ref i, ref p, ref skräplista, ref spelarensallakort, ref y, ref kortvärde, ref splittest);
                    return;
                }
                else
                {
                    Console.WriteLine("Kunde inte uppfatta det du sprev, försök igen");
                }
            }

        }


        static bool Kortjämförare(string kort, string[] lista)
        {
            foreach (string e in lista)
            {
                if (e == kort)
                {
                    return false;
                }
            }
            return true;
        }
        static string Kortutdelare(Random slump, ref string[] lista, ref int äss, ref int i, ref string[] lista2, ref int o, ref int värde, ref int kortvärde, ref int[] splittest)
        {
            while (true)
            {
                if (i == 52)
                {
                    i = 0;
                    int b = 0;
                    foreach (string e in lista)
                    {
                        lista[b] = "";
                        b++;
                    }
                }
                string kortnamn = "";
                kortvärde = Kortgivare(slump);
                splittest[o] = kortvärde;
                string kort = Kort(kortvärde, slump, ref kortnamn);
                if (Kortjämförare(kort, lista) == true)
                {
                    värde += Kortvärde(kortvärde);
                    lista[i] = kort;
                    i++;
                    lista2[o] = kort;
                    o++;

                    if (kortvärde == 1)
                    {
                        äss++;
                    }
                    return kort;
                }
            }
        }
        static string Skaskrivas(string[] lista, int i)
        {
            string utskrift = "";
            for (; i > 0; i--)
            {
                if (utskrift == "")
                {
                    utskrift = Convert.ToString(lista[i - 1]);
                }
                else if (i > 1)
                {
                    utskrift = utskrift + ", " + Convert.ToString(lista[i - 1]);
                }
                else if (i == 1)
                {
                    utskrift = utskrift + " och " + Convert.ToString(lista[i - 1]);
                }
            }
            return utskrift;
        }
    }
}
