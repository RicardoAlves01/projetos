﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using Loja.Modelos;


namespace Loja.DAL

{

    public class ProdutosDAL

    {

        public ArrayList ProdutosEmFalta()

        {


            SqlConnection cn = new SqlConnection(Dados.StringDeConexao);

            SqlCommand cmd = new SqlCommand("Select * from Produtos Where Estoque < 10", cn);


            cn.Open();


            SqlDataReader dr = cmd.ExecuteReader();

            ArrayList lista = new ArrayList();

            while (dr.Read())

            {

                ProdutoInformation produto = new ProdutoInformation();

                produto.Codigo = Convert.ToInt32(dr["codigo"]);

                produto.Nome = dr["nome"].ToString();

                produto.Estoque = Convert.ToInt32(dr["estoque"]);

                produto.Preco = Convert.ToDecimal(dr["preco"]);

                lista.Add(produto);


            }


            dr.Close();

            cn.Close();


            return lista;

        }


        public void Incluir(ProdutoInformation produto)

        {

            //conexao

            SqlConnection cn = new SqlConnection();

            try

            {

                cn.ConnectionString = Dados.StringDeConexao;

                //command

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cn;

                cmd.CommandText = "insert into Produtos(nome,preco,estoque) values (@nome,@preco,@estoque); select @@IDENTITY;";


                cmd.Parameters.AddWithValue("@nome", produto.Nome);

                cmd.Parameters.AddWithValue("@preco", produto.Preco);

                cmd.Parameters.AddWithValue("@estoque", produto.Estoque);


                cn.Open();

                produto.Codigo = Convert.ToInt32(cmd.ExecuteScalar());


            }

            catch (SqlException ex)

            {

                throw new Exception("Servidor SQL Erro: " + ex.Number);

            }

            catch (Exception ex)

            {

                throw new Exception(ex.Message);

            }

            finally

            {

                cn.Close();

            }


        }

        public void Alterar(ProdutoInformation produto)

        {

            //conexao

            SqlConnection cn = new SqlConnection();

            try

            {

                cn.ConnectionString = Dados.StringDeConexao;

                //command

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cn;

                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "update Produtos set nome = @nome, preco = @preco,  estoque = @estoque where codigo = @codigo;";

                cmd.Parameters.AddWithValue("@codigo", produto.Codigo);

                cmd.Parameters.AddWithValue("@nome", produto.Nome);

                cmd.Parameters.AddWithValue("@preco", produto.Preco);

                cmd.Parameters.AddWithValue("@estoque", produto.Estoque);


                cn.Open();

                cmd.ExecuteNonQuery();

            }

            catch (SqlException ex)

            {

                throw new Exception("Servidor SQL Erro: " + ex.Number);

            }

            catch (Exception ex)

            {

                throw new Exception(ex.Message);

            }

            finally

            {

                cn.Close();

            }


        }


        //######Metodo Alterar usando Procedure#########################
        //Metodo Alterar usando Procedure
        //##################################
        //public void Alterar(ProdutoInformation produto)

        //{

        //    //conexao

        //    SqlConnection cn = new SqlConnection();

        //    try

        //    {

        //        cn.ConnectionString = Dados.StringDeConexao;

        //        //command

        //        SqlCommand cmd = new SqlCommand();

        //        cmd.Connection = cn;

        //        cmd.CommandText = "AlterarProduto";

        //        cmd.CommandType = CommandType.StoredProcedure;


        //        cmd.Parameters.AddWithValue("@codigo", produto.Codigo);

        //        cmd.Parameters.AddWithValue("@nome", produto.Nome);

        //        cmd.Parameters.AddWithValue("@preco", produto.Preco);

        //        cmd.Parameters.AddWithValue("@estoque", produto.Estoque);

        //        cmd.Parameters.Add("@valorEstoque", SqlDbType.Int);

        //        cmd.Parameters["@valorEstoque"].Direction = ParameterDirection.Output;



        //        cn.Open();

        //        cmd.ExecuteNonQuery();


        //        decimal valorEstoque = Convert.ToDecimal(cmd.Parameters["@valorEstoque"]);

        //        if (valorEstoque < 500)

        //        {

        //            throw new Exception("Atenção! Valor baixo no estoque");

        //        }


        //    }

        //    catch (SqlException ex)

        //    {

        //        throw new Exception("Servidor SQL Erro: " + ex.Number);

        //    }

        //    catch (Exception ex)

        //    {

        //        throw new Exception(ex.Message);

        //    }

        //    finally

        //    {

        //        cn.Close();

        //    }


        //}


        public void Excluir(int codigo)

        {
            //conexao
            SqlConnection cn = new SqlConnection();

            try

            {

                cn.ConnectionString = Dados.StringDeConexao;

                //command

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cn;

                cmd.CommandText = "delete from Produtos where codigo = " + codigo;


                cn.Open();

                int resultado = cmd.ExecuteNonQuery();

                if (resultado != 1)

                {

                    throw new Exception("Não foi possível excluir o produto " + codigo);

                }

            }

            catch (SqlException ex)

            {

                throw new Exception("Servidor SQL Erro:" + ex.Number);

            }

            catch (Exception ex)

            {

                throw new Exception(ex.Message);

            }

            finally

            {

                cn.Close();

            }

        }


        public DataTable Listagem()

        {

            DataTable tabela = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter("select * from produtos", Dados.StringDeConexao);

            da.Fill(tabela);

            return tabela;

        }


    }

}