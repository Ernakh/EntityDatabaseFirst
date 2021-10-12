using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEntityDatabaseFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            //Scaffold-DbContext "Data Source=localhost;initial Catalog=EntityDatabaseFirst;User ID=teste;password=teste;language=Portuguese;Trusted_Connection=True;"
            //Provider: SQLOLEDB

            /*
             
                Add-Migration
                Drop-Database
                Get-DbContext
                Get-Migration
                Remove-Migration
                Scaffold-DbContext
                Script-Migration
                Update-Database
             
             
             */

            Console.WriteLine("Informe " +
               "1 para criar uma pessoa, " +
               "2 para alterar nome, " +
               "3 para inserir email, " +
               "4 para excluir a pessoa, " +
               "5 para consultar todos, " +
               "6 para consultar por ID");

            int op = int.Parse(Console.ReadLine());

            EntityDatabaseFirstContext contexto = new EntityDatabaseFirstContext();

            switch (op)
            {
                case 1:
                    try
                    {
                        Console.WriteLine("Insira o nome:");
                        Pessoa p = new Pessoa();
                        p.Nome = Console.ReadLine();
                        Console.WriteLine("Insira o email:");
                        string temp = Console.ReadLine();
                        p.Emails = new List<Email>()
                        {
                            new Email()
                            {
                                Email1 = temp
                            }
                        };

                        contexto.Pessoas.Add(p);
                        contexto.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        Console.WriteLine("Informe o id da pessoa");
                        int id = int.Parse(Console.ReadLine());
                        Pessoa pAlt = contexto.Pessoas.Find(id);
                        Console.WriteLine("Insira o nome:");
                        pAlt.Nome = Console.ReadLine();
                        contexto.Pessoas.Update(pAlt);
                        contexto.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 3:
                    Console.WriteLine("Informe o id da pessoa");
                    int idEm = int.Parse(Console.ReadLine());
                    Pessoa pEm = contexto.Pessoas.Find(idEm);
                    Console.WriteLine("Insira o novo email:");
                    string emailTemp = Console.ReadLine();
                    pEm.Emails = new List<Email>()
                    {
                        new Email()
                        {
                            Email1 = emailTemp
                        }
                    };

                    contexto.Pessoas.Update(pEm);
                    contexto.SaveChanges();

                    break;
                case 4:
                    Console.WriteLine("Informe o id da pessoa");
                    int idExc = int.Parse(Console.ReadLine());
                    Pessoa pExc = contexto.Pessoas.Find(idExc);

                    Console.WriteLine("Confirma a exclusão de " + pExc.Nome);
                    Console.WriteLine("E dos emails: ");

                    foreach (Email itemEmail in pExc.Emails)
                    {
                        Console.WriteLine("   " + itemEmail.Email1);
                    }

                    Console.WriteLine("1 - Sim, 2 - Não ");

                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        contexto.Pessoas.Remove(pExc);
                        contexto.SaveChanges();
                    }
                    else if (int.Parse(Console.ReadLine()) == 2)
                    {
                        return;
                    }

                    break;
                case 5:
                    List<Pessoa> lista = new List<Pessoa>();
                    lista = getAllPessoas(contexto);
                    foreach (Pessoa pessoaItem in lista)
                    {
                        Console.WriteLine(pessoaItem.Nome);
                        if (pessoaItem.Emails != null)
                        {
                            foreach (Email emailItem in pessoaItem.Emails)
                            {
                                Console.WriteLine("    " + emailItem.Email1);
                            }

                            Console.WriteLine();
                        }
                    }

                    break;
                case 6:
                    Console.WriteLine("Informe o id da pessoa");
                    int idPessoa = int.Parse(Console.ReadLine());
                    Pessoa pessoa = getPessoa(idPessoa, contexto);
                    Console.WriteLine(pessoa.Nome);

                    if (pessoa.Emails != null)
                    {
                        foreach (Email item in pessoa.Emails)
                        {
                            Console.WriteLine("    " + item.Email1);
                        }
                    }

                    break;
                default:
                    break;
            }

            static Pessoa getPessoa(int id, EntityDatabaseFirstContext contexto)
            {
                return contexto.Pessoas
                    .Include(p => p.Emails)
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        private static List<Pessoa> getAllPessoas(EntityDatabaseFirstContext contexto)
        {
            return
            (from Pessoa p in contexto.Pessoas
             select p).Include(e => e.Emails).ToList<Pessoa>();
        }
    }
}
