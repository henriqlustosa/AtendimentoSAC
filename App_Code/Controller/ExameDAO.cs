using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ExameDAO
/// </summary>
public class ExameDAO
{
    public ExameDAO()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void GravaExamesPorPedidos(List<Exame> exames, int _cod_pedido)
    {
        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["gtaConnectionString"].ToString()))
        {
            SqlCommand cmm = new SqlCommand();
            cmm.Connection = cnn;
            cnn.Open();
            SqlTransaction mt = cnn.BeginTransaction();
            cmm.Transaction = mt;
            try
            {

                foreach (Exame exame in exames)
                { 
                cmm.CommandText = "Insert into pedido_exame (cod_exame, cod_pedido)"
            + " values ('"
                                + exame.cod_exame + "','"
                                + _cod_pedido 
                                + "');";
                cmm.ExecuteNonQuery();

                
                
               
                }
                mt.Commit();
                mt.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                mt.Rollback();
            }
        }
    }

    public static List<Exame> listaExame()
    {
        var listaEspec = new List<Exame>();
        using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["gtaConnectionString"].ToString()))
        {
            SqlCommand cmm = cnn.CreateCommand();
            cmm.CommandText = "SELECT cod_exame, descricao_exame, status_exame " +
                             " FROM [hspmAtendimento_Call].[dbo].[exame] " +
                             " ORDER BY cod_exame";

            try
            {
                cnn.Open();

                SqlDataReader dr1 = cmm.ExecuteReader();

                while (dr1.Read())
                {
                    Exame exm = new Exame();
                    exm.cod_exame = dr1.GetInt32(0);
                    exm.descricao_exame = dr1.GetString(1);
                    exm.status_exame = dr1.GetString(2);
                    listaEspec.Add(exm);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

        }
        return listaEspec;
    }
}