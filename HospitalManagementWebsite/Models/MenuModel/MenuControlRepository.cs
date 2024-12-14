using HospitalManagementWebsite.Models.MenuModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

public class MenuControlRepository
{
    private string connectionString = ConfigurationManager.ConnectionStrings["CON"].ConnectionString;

    // Get all menu controls
    public List<MenuControl> GetAllMenuControls()
    {
        List<MenuControl> menuControls = new List<MenuControl>();

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM MenuControl", conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader()) // Ensure reader is disposed properly
                {
                    while (reader.Read())
                    {
                        menuControls.Add(new MenuControl
                        {
                            MenuID = Convert.ToInt32(reader["MenuID"]),
                            DepartmentCode = reader["DepartmentCode"].ToString(),
                            DesignationCode = reader["DesignationCode"].ToString(),
                            MenuName = reader["MenuName"].ToString(),
                            MenuDescription = reader["MenuDescription"].ToString(),
                            PageLink = reader["PageLink"].ToString(),
                            ImportanceLevel = Convert.ToInt32(reader["ImportanceLevel"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"]),
                            CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                            Icon = reader["Icon"].ToString(),
                            ExtraColumn1 = reader["ExtraColumn1"].ToString(),
                            ExtraColumn2 = reader["ExtraColumn2"].ToString()
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as necessary
            throw new ApplicationException("Error while fetching menu controls.", ex);
        }

        return menuControls;
    }

    // Get a specific menu by ID
    public MenuControl GetMenuById(int menuID)
    {
        MenuControl menu = null;

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM MenuControl WHERE MenuID = @MenuID", conn);
                cmd.Parameters.AddWithValue("@MenuID", menuID);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader()) // Ensure reader is disposed properly
                {
                    if (reader.Read())
                    {
                        menu = new MenuControl
                        {
                            MenuID = Convert.ToInt32(reader["MenuID"]),
                            DepartmentCode = reader["DepartmentCode"].ToString(),
                            DesignationCode = reader["DesignationCode"].ToString(),
                            MenuName = reader["MenuName"].ToString(),
                            MenuDescription = reader["MenuDescription"].ToString(),
                            PageLink = reader["PageLink"].ToString(),
                            ImportanceLevel = Convert.ToInt32(reader["ImportanceLevel"]),
                            IsActive = Convert.ToBoolean(reader["IsActive"]),
                            CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                            Icon = reader["Icon"].ToString(),
                            ExtraColumn1 = reader["ExtraColumn1"].ToString(),
                            ExtraColumn2 = reader["ExtraColumn2"].ToString()
                        };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as necessary
            throw new ApplicationException("Error while fetching menu control by ID.", ex);
        }

        return menu;
    }

    // Add a new menu
    public void AddMenu(MenuControl menu)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO MenuControl 
                    (DepartmentCode, DesignationCode, MenuName, MenuDescription, PageLink, ImportanceLevel, IsActive, CreatedOn, Icon) 
                    VALUES 
                    (@DepartmentCode, @DesignationCode, @MenuName, @MenuDescription, @PageLink, @ImportanceLevel, @IsActive, @CreatedOn, @Icon)", conn);

                cmd.Parameters.AddWithValue("@DepartmentCode", menu.DepartmentCode);
                cmd.Parameters.AddWithValue("@DesignationCode", menu.DesignationCode);
                cmd.Parameters.AddWithValue("@MenuName", menu.MenuName);
                cmd.Parameters.AddWithValue("@MenuDescription", menu.MenuDescription);
                cmd.Parameters.AddWithValue("@PageLink", menu.PageLink);
                cmd.Parameters.AddWithValue("@ImportanceLevel", menu.ImportanceLevel);
                cmd.Parameters.AddWithValue("@IsActive", menu.IsActive);
                cmd.Parameters.AddWithValue("@CreatedOn", menu.CreatedOn);
                cmd.Parameters.AddWithValue("@Icon", menu.Icon);
                //cmd.Parameters.AddWithValue("@ExtraColumn1", menu.ExtraColumn1);
                //cmd.Parameters.AddWithValue("@ExtraColumn2", menu.ExtraColumn2);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as necessary
            throw new ApplicationException("Error while adding a new menu.", ex);
        }
    }

    // Update an existing menu
    public void UpdateMenu(MenuControl menu)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE MenuControl SET 
                    DepartmentCode = @DepartmentCode, 
                    DesignationCode = @DesignationCode, 
                    MenuName = @MenuName, 
                    MenuDescription = @MenuDescription, 
                    PageLink = @PageLink, 
                    ImportanceLevel = @ImportanceLevel, 
                    IsActive = @IsActive, 
                    Icon = @Icon, 
                    ExtraColumn1 = @ExtraColumn1, 
                    ExtraColumn2 = @ExtraColumn2 
                    WHERE MenuID = @MenuID", conn);

                cmd.Parameters.AddWithValue("@MenuID", menu.MenuID);
                cmd.Parameters.AddWithValue("@DepartmentCode", menu.DepartmentCode);
                cmd.Parameters.AddWithValue("@DesignationCode", menu.DesignationCode);
                cmd.Parameters.AddWithValue("@MenuName", menu.MenuName);
                cmd.Parameters.AddWithValue("@MenuDescription", menu.MenuDescription);
                cmd.Parameters.AddWithValue("@PageLink", menu.PageLink);
                cmd.Parameters.AddWithValue("@ImportanceLevel", menu.ImportanceLevel);
                cmd.Parameters.AddWithValue("@IsActive", menu.IsActive);
                cmd.Parameters.AddWithValue("@Icon", menu.Icon);
                cmd.Parameters.AddWithValue("@ExtraColumn1", menu.ExtraColumn1);
                cmd.Parameters.AddWithValue("@ExtraColumn2", menu.ExtraColumn2);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as necessary
            throw new ApplicationException("Error while updating menu control.", ex);
        }
    }

    // Delete a menu by ID
    public void DeleteMenu(int menuID)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM MenuControl WHERE MenuID = @MenuID", conn);
                cmd.Parameters.AddWithValue("@MenuID", menuID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as necessary
            throw new ApplicationException("Error while deleting menu control.", ex);
        }
    }
}
