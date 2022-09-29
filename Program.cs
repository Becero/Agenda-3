using System;
using System.IO;
using System.Threading;

namespace AgendaLP2A4
{
    internal class Program
    {
        static int Menu()
        {
            int opcao = -1;
            do
            {
                Console.Clear();
                Console.WriteLine("============== MENU ==============");
                Console.WriteLine("| ..................... SAIR - 0 |");
                Console.WriteLine("| .................CADASTRAR - 1 |");
                Console.WriteLine("| ................... EDITAR - 2 |");
                Console.WriteLine("| .................. EXCLUIR - 3 |");
                Console.WriteLine("| ............ MOSTRAR TODOS - 4 |");
                Console.WriteLine("| .......... BUSCAR POR NOME - 5 |");
                Console.WriteLine("==================================");

                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Voce nao digitou um texto valido");
                    Console.WriteLine("Digite algo pra continuar");
                    Console.ReadKey();
                }

            } while (opcao <0 || opcao >5);
               
            return opcao;
        }
        static Paciente Cadastro()
        {
            Console.WriteLine("Entre com o nome do paciente: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Entre com o telefone ((00) 00000-0000):");
            string telefone = Console.ReadLine();
            Console.WriteLine("Entre com o email do paciente: ");
            string email = Console.ReadLine();
            Console.WriteLine("Entre com o cpf do paciente: ");
            string cpf = Console.ReadLine();

            try
            {
                Paciente novo = new Paciente(nome, telefone, email, cpf);
                return novo;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        static void EscritaArquivo(Paciente[] pac, int qtd)
        {
            string caminho = "BancoDeDados.txt";
            FileStream fs = new FileStream(caminho, FileMode.Create);
            try
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    for (int i = 0; i < qtd; i++)
                    {
                        writer.WriteLine(pac[i].DadosParaArquivo());
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("FALHA AO CARREGAR DADOS NO BANCO!");
            }
            finally
            {
                if (fs != null)
                    fs.Dispose();
            }
        }

        static void LeituraArquivo(Paciente[] pac, ref int qtd)
        {
            // Ler e mostrar cada linha do arquivo.
            string line, caminho = "BancoDeDados.txt";
            StreamReader sr = null;
            try
            {
                if (File.Exists(caminho))
                {
                    sr = new StreamReader(caminho);
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] dados = line.Split(';');
                        pac[qtd++] = new Paciente(dados[0], dados[1], dados[2], dados[3]);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("FALHA AO LER DADOS DO BANCO!");
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        static void Main(string[] args)
        {
            Paciente[] pac = new Paciente[100];
            int opcao, qtd = 0, i;

            LeituraArquivo(pac, ref qtd);

            do
            {
                opcao = Menu();

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Obrigado por usar o sistema!");
                        EscritaArquivo(pac, qtd);
                        break;
                    case 1:
                        if (qtd == 100)
                            Console.WriteLine("Entre em contato com o " +
                                "Administrador para comprar mais espaço!");
                        else
                        {
                            Paciente novo = Cadastro();
                            if (novo != null)
                            {
                                Console.WriteLine("CADASTRADO COM SUCESSO!");
                                pac[qtd] = novo;
                                qtd++;
                            }
                            else
                                Console.WriteLine("HOUVE UMA FALHA AO CADASTRAR!");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Entre com o nome do paciente a ser editado: ");
                        string nomeEditar = Console.ReadLine().ToLower();
                        for (i = 0; i < qtd; i++)
                        {
                            if (pac[i].Nome.ToLower().Contains(nomeEditar))
                            {
                                Console.WriteLine("\n======== EDITANDO PACIENTE {0}========", i + 1);
                                Console.WriteLine("NOME ATUAL: " + pac[i].Nome + "\n" +
                                    "DIGITE UM NOVO NOME OU APENAS DÊ ENTER PARA CONTINUAR...");
                                string resp = Console.ReadLine();

                                if (resp.Length != 0) pac[i].Nome = resp;

                                Console.WriteLine("TELEFONE ATUAL: " + pac[i].Telefone +
                                    "\nDIGITE UM NOVO TELEFONE OU APENAS DÊ ENTER PARA CONTINUAR...");
                                resp = Console.ReadLine();
                                if (resp.Length != 0) pac[i].Telefone = resp;
                                break;
                            }
                        }
                        if (i == qtd)
                            Console.WriteLine("NENHUM PACIENTE COM O NOME \"{0}\" FOI ENCONTRADO!", nomeEditar);
                        break;
                    case 3:
                        Console.WriteLine("Entre com o nome do paciente a ser excluído: ");
                        string nomeExcluir = Console.ReadLine().ToLower();
                        for (i = 0; i < qtd; i++)
                        {
                            if (pac[i].Nome.ToLower().Contains(nomeExcluir))
                            {
                                pac[i].Nome = pac[qtd - 1].Nome;
                                pac[i].Telefone = pac[qtd - 1].Telefone;
                                qtd--;
                                Console.WriteLine("EXCLUÍDO COM SUCESSO!");
                                break;
                            }
                        }
                        if (i == qtd)
                            Console.WriteLine("NENHUM PACIENTE COM O NOME \"{0}\" FOI " +
                                "ENCONTRADO!", nomeExcluir);
                        break;
                    case 4:
                        for (i = 0; i < qtd; i++)
                        {
                            Console.WriteLine("\n======== PACIENTE {0}========", i + 1);
                            Console.WriteLine(pac[i].ToString());
                        }
                        Console.WriteLine("\n=============================");
                        break;
                    case 5:
                        Console.WriteLine("Entre com o nome a ser buscado: ");
                        string nomeBuscado = Console.ReadLine().ToLower();
                        bool validacao = true;
                        for (i = 0; i < qtd; i++)
                        {
                            if (pac[i].Nome.ToLower().Contains(nomeBuscado))
                            {
                                Console.WriteLine("\n======== PACIENTE {0}========", i + 1);
                                Console.WriteLine(pac[i].ToString());
                                validacao = false;
                            }
                        }
                        if (validacao)
                            Console.WriteLine("NENHUM PACIENTE COM O NOME \"{0}\" " +
                                "FOI ENCONTRADO!", nomeBuscado);
                        break;
                    default:
                        Console.WriteLine("Escolha uma opção válida!");
                        break;
                }
                Console.WriteLine("Aperte algo para continuar...");
                Console.ReadKey();
                Console.Clear();

            } while (opcao != 0);
        }
    }
}

