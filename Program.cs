using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01
{
    class Program
    {
        static void Main(string[] args)
        {
            int Option = 1;
            while (Option != 0)
            {
                Option = MainMenu();
                switch (Option)
                {
                    case 1:
                        int CustomerID = CustomerAuthentication();
                        if (CustomerID > 0)
                        {
                            int COption;
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Welcome Customer");
                                COption = CustomerMenu(CustomerID);
                                switch (COption)
                                {
                                    case 1:
                                        ShowOrders(CustomerID);
                                        ActionRegistery($"{CustomerID}", "Show Orders", "Secceded");
                                        break;
                                    case 2:
                                        ShowProducts();
                                        ActionRegistery($"{CustomerID}", "Show Products", "Secceded");
                                        break;
                                    case 3:
                                        InsertOrder(CustomerID);
                                        break;

                                }
                            }
                            while (COption != 0);                        
                        }
                        else
                        {
                            Console.WriteLine("User Name or Pass Not Found. Try Again");
                        }
                        break;
                    case 2:
                        CreateCustomer();
                        Console.Clear();
                        break;
                    case 3:
                        int SupplierID = SupplierAuthentication();
                        if (SupplierID > 0)
                        {
                            int SOption;
                            do
                            {
                                Console.Clear();
                                Console.WriteLine("Welcome Supplier");
                                SOption = SupplierMenu(SupplierID);
                                switch (SOption)
                                {
                                    case 1:
                                        InsertProduct(SupplierID);
                                        break;
                                    case 2:
                                        ImportSupplierInfo(SupplierID);
                                        break;
                                }
                            }
                            while (SOption !=0);
                        }
                        else
                        {
                            Console.WriteLine("User Name or Pass Not Found. Try Again");
                        }
                        break;
                    case 4:
                        CreateSupplier();
                        Console.Clear();
                        break;
                    case 5:
                        ShowActionRegistery();
                        break;
                    case 0:
                        break;

                }
            }
            Console.WriteLine("GoodBye!");

        }

        private static void InsertProduct(int SupplierID)
        {
            bool flag;
            do
            {
                try
                {
                    int temp = 0;
                    Console.WriteLine("Product Adder");
                    Console.Write("Enter Product Name:");
                    string productName = Console.ReadLine();
                    List<Product> listP = ImportProduct();
                    foreach (Product item in listP)
                    {
                        if (Equals(item.ProductName, productName) && item.SupplierID != SupplierID)
                        {
                            ActionRegistery($"{SupplierID}", "Insert Product", "Product Exist");
                            Console.WriteLine("Product Already Exist From Other Supplier, Sorry.");
                            Console.WriteLine("Press Any Key To Return");
                            Console.ReadLine();
                            temp = 1;
                            flag = false;
                            break;
                        }
                    }
                    if (temp == 1)
                        break;
                    foreach (Product item in listP)
                    {
                        if (Equals(item.ProductName, productName) && item.SupplierID == SupplierID)
                        {
                            ActionRegistery($"{SupplierID}", "Insert Product", "MORE SUPPLY!");
                            UpdateProduct(item);
                            temp= 2;
                            flag = false;
                            break;
                        }
                    }
                    if (temp == 2)
                        break;
                    NewProduct(productName, SupplierID);
                    ActionRegistery($"{SupplierID}", "Insert Product", "New Product");
                    flag = false;
                }
                catch (FormatException e)
                {
                    Console.Clear();
                    Console.WriteLine("Format Not Correct");
                    flag = true;
                }
            }
            while (flag);
        }

        private static void NewProduct(string productName, int SupplierID)
        {
            bool flag;
            do
            {
                try
                {
                    Console.Write("Enter Supply: ");
                    int supply = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter Cost: ");
                    int cost = Convert.ToInt32(Console.ReadLine());
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO PRODUCT (PRODUCT_NAME, SUPPLIER_ID, COST, SUPPLY) VALUES (@PRODUCT_NAME, @SUPPLIER_ID, @COST, @SUPPLY)";
                    cmd.Parameters.Add("@PRODUCT_NAME", productName);
                    cmd.Parameters.Add("@SUPPLIER_ID", SupplierID);
                    cmd.Parameters.Add("@COST", cost);
                    cmd.Parameters.Add("@SUPPLY", supply);
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    flag = false;
                    Console.WriteLine("Product Added");
                    Console.WriteLine("Press Any Key To Return");
                    Console.ReadLine();

                }
                catch (FormatException e)
                {
                    Console.Clear();
                    Console.WriteLine("Format Not Correct");
                    flag = true;
                }
            }
            while (flag);
        }

        private static void UpdateProduct(Product item)
        {
            bool flag;
            do
            {
                try
                {
                    int supply;
                    Console.WriteLine("Product Already Exist From You, We just Added The Supply.");
                    Console.Write("Enter Supply");
                    supply = Convert.ToInt32(Console.ReadLine());
                    int newSupply = supply + item.Supply;
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE PRODUCT SET SUPPLY = @newsupply WHERE PRODUCT_ID = @PRODUCTID";
                    cmd.Parameters.Add("@newsupply", newSupply);
                    cmd.Parameters.Add("@PRODUCTID", item.ProductID);
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    flag = false;
                    Console.WriteLine("Product Updated");
                    Console.WriteLine("Press Any Key To Return");
                    Console.ReadLine();
                }
                catch (FormatException e)
                {
                    Console.Clear();
                    Console.WriteLine("Format Not Correct");
                    flag = true;
                }
            }
            while (flag);
            
        }

        private static void ShowOrders(int CustomerID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ID, CUSTOMER_ID, NUMBER_OF_ORDERS, TOTAL_COST, PRODUCT_NAME FROM ORDERS AS O JOIN PRODUCT AS P ON P.PRODUCT_ID = O.PRODUCT_ID AND O.CUSTOMER_ID =@CUSTOMER_ID";
            cmd.Parameters.Add("@CUSTOMER_ID", CustomerID);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
            while (reader.Read() == true)
            {
                Console.WriteLine($"ID: {reader["ID"]}, Number Of Oreders: {reader["NUMBER_OF_ORDERS"]}, Total Cost: {reader["TOTAL_COST"]}, Product Name: {reader["PRODUCT_NAME"]}");
            }
            cmd.Connection.Close();
            Console.WriteLine("Press Enter To Return To Main Menu");
            Console.ReadLine();
        }

        private static void ImportSupplierInfo(int SupplierID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM PRODUCT WHERE SUPPLIER_ID = @supplierID";
            cmd.Parameters.Add("@supplierID", SupplierID);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
            List<Product> list = new List<Product>();
            while (reader.Read() == true)
            {
                Console.WriteLine($"ID: {reader["PRODUCT_ID"]}, Name: {reader["PRODUCT_NAME"]}, Supplier ID: {reader["SUPPLIER_ID"]}, Cost: {reader["COST"]}, Supply: {reader["SUPPLY"]}");
            }
            cmd.Connection.Close();
            Console.WriteLine("Press Enter To Return To Main Menu");
            Console.ReadLine();
        }

        private static void InsertOrder(int CustomerID)
        {
            bool flag;
            do
            {
                try
                {
                    Console.WriteLine("Orders:");
                    Console.Write("Enter Product Name:");
                    string name = Convert.ToString(Console.ReadLine());
                    Console.Write("Enter How Much You Want To Order:");
                    int numberOfOrders = Convert.ToInt32(Console.ReadLine());
                    Product P = new Product(name, numberOfOrders);
                    List<Product> listP = ImportProduct();
                    int orderinsert = 0;
                    foreach (Product item in listP)
                    {
                        if (Equals(item.ProductName, P.ProductName))
                        {
                            if (item.Supply > 0)
                            {
                                if (item.Supply > P.Supply)
                                {
                                    int newsupply = item.Supply - P.Supply;
                                    int totalcost = item.Cost * P.Supply;
                                    SqlCommand cmd = new SqlCommand();
                                    cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
                                    cmd.Connection.Open();
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "INSERT INTO ORDERS (CUSTOMER_ID, PRODUCT_ID, NUMBER_OF_ORDERS, TOTAL_COST) VALUES (@CUSTOMER_ID, @PRODUCT_ID, @NUMBER_OF_ORDERS, @TOTAL_COST)";
                                    cmd.Parameters.Add("@CUSTOMER_ID", CustomerID);
                                    cmd.Parameters.Add("@PRODUCT_ID", item.ProductID);
                                    cmd.Parameters.Add("@NUMBER_OF_ORDERS", P.Supply);
                                    cmd.Parameters.Add("@TOTAL_COST", totalcost);
                                    cmd.ExecuteNonQuery();
                                    cmd.Connection.Close();
                                    cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
                                    cmd.Connection.Open();
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "UPDATE PRODUCT SET SUPPLY = @newsupply WHERE PRODUCT_ID = @PRODUCTID";
                                    cmd.Parameters.Add("@newsupply", newsupply);
                                    cmd.Parameters.Add("@PRODUCTID", item.ProductID);
                                    cmd.ExecuteNonQuery();
                                    cmd.Connection.Close();
                                    orderinsert = 1;
                                    break;
                                }
                                orderinsert = 4;
                                break;
                            }
                            orderinsert = 3;
                            break;
                        }
                        orderinsert = 2;
                    }
                    flag = false;
                    InsertMenu(orderinsert, CustomerID);
                }
                catch (FormatException e)
                {
                    Console.Clear();
                    Console.WriteLine("Format Not Correct");
                    flag = true;
                }
            }
            while (flag);
        }

        private static List<Product> ImportProduct()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM PRODUCT";
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
            List<Product> listP = new List<Product>();
            while (reader.Read() == true)
            {
                Product p = new Product((int)reader["PRODUCT_ID"], (string)reader["PRODUCT_NAME"], (int)reader["SUPPLIER_ID"], (int)reader["COST"], (int)reader["SUPPLY"]);
                listP.Add(p);
            }
            cmd.Connection.Close();
            return listP;
        }

        private static void InsertMenu(int orderinsert, int CustomerID)
        {
            if (orderinsert == 1)
            {
                ActionRegistery($"{CustomerID}", "Make Order", "Seccseded");
                Console.WriteLine("Order Seccesfuly Entered!");
                Console.WriteLine("Press Enter To Go Back To Menu");
                Console.ReadLine();
            }
            if (orderinsert == 2)
            {
                ActionRegistery($"{CustomerID}", "Make Order", "No Such Product");
                Console.WriteLine("No Such Product");
                Console.WriteLine("Press Enter To Go Back To Menu");
                Console.ReadLine();
            }
            if (orderinsert == 3)
            {
                ActionRegistery($"{CustomerID}", "Make Order", "Order Must Be Greater Then 0");
                Console.WriteLine("Order Must Be Greater Then 0");
                Console.WriteLine("Press Enter To Go Back To Menu");
                Console.ReadLine();
            }
            if (orderinsert == 4)
            {
                ActionRegistery($"{CustomerID}", "Make Order", "Not Enough Supply");
                Console.WriteLine("Not Enough Supply");
                Console.WriteLine("Press Enter To Go Back To Menu");
                Console.ReadLine();
            }
        }

        private static void ShowProducts()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM PRODUCT";
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
            while (reader.Read() == true)
            {
                Console.WriteLine($"ID: {reader["PRODUCT_ID"]}, Name: {reader["PRODUCT_NAME"]}, Supplier ID: {reader["SUPPLIER_ID"]}, Cost: {reader["COST"]}, Supply: {reader["SUPPLY"]}");
            }
            cmd.Connection.Close();
            Console.WriteLine("Press Enter To Return To Main Menu");
            Console.ReadLine();
        }

        private static int SupplierMenu(int SupplierID)
        {            
            bool flag;
            int x = 1;
            do
            {
                try
                {
                    Console.WriteLine("Options:");
                    Console.WriteLine("1) Add Product");
                    Console.WriteLine("2) View My Products");
                    Console.WriteLine("0) EXIT");
                    x = Convert.ToInt32(Console.ReadLine());
                    if (x > 2 || x < 0)
                    {
                        throw new OutOfRangeOfMenu();
                    }
                    flag = false;
                    Console.Clear();
                }
                catch (OutOfRangeOfMenu e)
                {
                    ActionRegistery($"{SupplierID}", "SupplierMenu", "Failed");
                    Console.Clear();
                    Console.WriteLine("Out Of Range Of The Options In Menu");
                    flag = true;
                }
                catch (FormatException e)
                {
                    ActionRegistery($"{SupplierID}", "SupplierMenu", "Failed");
                    Console.Clear();
                    Console.WriteLine("Format Not Correct");
                    flag = true;
                }

            }
            while (flag);
            ActionRegistery($"{SupplierID}", "SupplierMenu", "Secceded");
            return x;
        }

        private static int CustomerMenu(int CustomerID)
        {            
            bool flag;
            int x = 1;
            do
            {
                try
                {
                    Console.WriteLine("Options:");
                    Console.WriteLine("1) My purchases");
                    Console.WriteLine("2) View Products");
                    Console.WriteLine("3) Oreder Product");
                    Console.WriteLine("0) EXIT");
                    x = Convert.ToInt32(Console.ReadLine());
                    if (x > 3 || x < 0)
                    {
                        throw new OutOfRangeOfMenu();
                    }
                    flag = false;
                    Console.Clear();
                }
                catch (OutOfRangeOfMenu e)
                {
                    ActionRegistery($"{CustomerID}", "Customer Menu", "Failed");
                    Console.Clear();
                    Console.WriteLine("Out Of Range Of The Options In Menu");
                    flag = true;
                }
                catch (FormatException e)
                {
                    ActionRegistery($"{CustomerID}", "Customer Menu", "Failed");
                    Console.Clear();
                    Console.WriteLine("Format Not Correct");
                    flag = true;
                }

            }
            while (flag);
            ActionRegistery($"{CustomerID}", "Customer Menu", "Secceded");
            return x;
        }

        private static void CreateSupplier()
        {
            bool flag;
            do
            {
                try
                {
                    Supplier s = new Supplier();
                    Console.WriteLine("New Supplier Creation!");
                    Console.WriteLine("Please Enter The Following");
                    Console.Write("User Name:");
                    s.UserName = Convert.ToString(Console.ReadLine());
                    Console.Write("Pass:");
                    s.Pass = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Compeny:");
                    s.Compeny = Convert.ToString(Console.ReadLine());

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO SUPPLIER (USERNAME, PASS, COMPENY) VALUES (@USERNAME, @PASS, @COMPENY)";
                    cmd.Parameters.Add("@USERNAME", s.UserName);
                    cmd.Parameters.Add("@PASS", s.Pass);
                    cmd.Parameters.Add("@COMPENY", s.Compeny);
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    Console.WriteLine($"{s.UserName} Was Seccesfuly Added!");
                    Console.Write("Press Any Key To Return To Main Menu");
                    Console.ReadLine();
                    flag = false;
                    ActionRegistery($"{s.UserName}", "Create Supplier", "Secceded");
                }
                catch (SqlException e)
                {
                    ActionRegistery("NON", "Create Supplier", "Failed");
                    Console.Clear();
                    Console.WriteLine("User Already Exists");
                    Console.WriteLine();
                    flag = true;
                }
                catch (FormatException e)
                {
                    ActionRegistery("NON", "Create Supplier", "Failed");
                    Console.Clear();
                    Console.WriteLine("Format Not Correct");
                    flag = true;
                }
            }
            while (flag);
        }

        private static int SupplierAuthentication()
        {
            bool flag;
            do
            {
                try
                {
                    Console.WriteLine("Supplier Authentication");
                    Console.Write("Enter User Name:");
                    string UserName = Console.ReadLine();
                    Console.Write("Enter Pass:");
                    int Pass = Convert.ToInt32(Console.ReadLine());

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT SUPPLIER_ID, USERNAME, PASS FROM SUPPLIER";
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read() == true)
                    {
                        var e = new
                        {
                            SupplierID = (int)reader["SUPPLIER_ID"],
                            UserName = (string)reader["USERNAME"],
                            Pass = (int)reader["PASS"]
                        };
                        if (e.UserName == UserName && e.Pass == Pass)
                        {
                            cmd.Connection.Close();
                            flag = false;
                            ActionRegistery(UserName, "Supplier Authentication", "Secceded");
                            return e.SupplierID;
                        }
                    }
                    flag = false;
                    cmd.Connection.Close();
                    ActionRegistery(UserName, "Supplier Authentication", "Failed");
                }
                catch (FormatException e)
                {
                    ActionRegistery("NON", "Supplier Authentication", "Failed");
                    Console.Clear();
                    Console.WriteLine("Format Not Correct");
                    flag = true;
                }
            }
            while (flag);

            return 0;
        }

        private static void CreateCustomer()
        {
            bool flag;
            do
            {
                try
                {
                    Customer c = new Customer();
                    Console.WriteLine("New Custumer Creation!");
                    Console.WriteLine("Please Enter The Following");
                    Console.Write("User Name:");
                    c.UserName = Convert.ToString(Console.ReadLine());
                    Console.Write("Pass:");
                    c.Pass = Convert.ToInt32(Console.ReadLine());
                    Console.Write("First Name:");
                    c.FirstName = Convert.ToString(Console.ReadLine());
                    Console.Write("Last Name:");
                    c.LastName = Convert.ToString(Console.ReadLine());
                    Console.Write("Credit Card:");
                    c.CreditCard = Convert.ToString(Console.ReadLine());

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO CUSTOMER (USERNAME, PASS, FIRSTNAME, LASTNAME, CREDITCARD) VALUES (@USERNAME, @PASS, @FIRSTNAME, @LASTNAME, @CREDITCARD)";
                    cmd.Parameters.Add("@USERNAME", c.UserName);
                    cmd.Parameters.Add("@PASS", c.Pass);
                    cmd.Parameters.Add("@FIRSTNAME", c.FirstName);
                    cmd.Parameters.Add("@LASTNAME", c.LastName);
                    cmd.Parameters.Add("@CREDITCARD", c.CreditCard);
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    ActionRegistery($"{c.UserName}", "Create Customer", "Secceded");
                    Console.WriteLine($"{c.UserName} Was Seccesfuly Added!");
                    Console.Write("Press Enter To Return To Main Menu");
                    Console.ReadLine();
                    flag = false;
                }
                catch(SqlException e)
                {
                    ActionRegistery("NON", "Create Customer", "failed");
                    Console.Clear();
                    Console.WriteLine("User Already Exists");
                    Console.WriteLine();
                    flag = true;
                }
                catch(FormatException e)
                {
                    ActionRegistery("NON", "Create Customer", "failed");
                    Console.Clear();
                    Console.WriteLine("Format Not Correct");
                    flag = true;
                }
            }
            while (flag);
        }

        private static int CustomerAuthentication()
        {
            string UserName;
            bool flag;
            do
            {
                try
                {
                    Console.WriteLine("Customer Authentication");
                    Console.Write("Enter User Name:");
                    UserName = Console.ReadLine();
                    Console.Write("Enter Pass:");
                    int Pass = Convert.ToInt32(Console.ReadLine());
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT CUSTOMER_ID, USERNAME, PASS FROM CUSTOMER";
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read() == true)
                    {
                        var e = new
                        {
                            CustomerID = (int)reader["CUSTOMER_ID"],
                            UserName = (string)reader["USERNAME"],
                            Pass = (int)reader["PASS"]
                        };
                        if (e.UserName == UserName && e.Pass == Pass)
                        {
                            cmd.Connection.Close();
                            flag = false;
                            ActionRegistery(UserName, "Customer Authentication", "Secceded");
                            return e.CustomerID;
                        }
                    }
                    flag = false;
                    cmd.Connection.Close();
                    ActionRegistery(UserName, "Customer Authentication", "User Not Existing");
                }
                catch (FormatException e)
                {
                    ActionRegistery("NON", "Customer Authentication", "failed");
                    Console.Clear();
                    Console.WriteLine("Format Not Correct");
                    flag = true;
                }
            }
            while (flag);
            return 0;
        }

        private static int MainMenu()
        {
            bool flag;
            int x = 1;
            do
            {
                try
                {
                    Console.WriteLine("Options:");
                    Console.WriteLine("1) Existing Customer");
                    Console.WriteLine("2) New Customer");
                    Console.WriteLine("3) Existing Supplier");
                    Console.WriteLine("4) New Supplier");
                    Console.WriteLine("5) Show Tracking Table");
                    Console.WriteLine("0) EXIT");
                    x = Convert.ToInt32(Console.ReadLine());
                    if (x > 6 || x < 0)
                    {
                        throw new OutOfRangeOfMenu();
                    }
                    flag = false;
                    Console.Clear();
                }
                catch (OutOfRangeOfMenu e)
                {
                    ActionRegistery("NON", "Main Menu", "failed");
                    Console.Clear();
                    Console.WriteLine("Out Of Range Of The Options In Menu");
                    flag = true;
                }
                catch (FormatException e)
                {
                    ActionRegistery("NON", "Main Menu", "failed");
                    Console.Clear();
                    Console.WriteLine("Format Not Correct");
                    flag = true;
                }
                
            }
            while (flag);
            ActionRegistery("NON", "Main Menu", "Secceded");
            return x;
        }

        private static void ShowActionRegistery()
        {
            Console.Clear();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT TOP(100) * FROM ACTION";
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
            while (reader.Read() == true)
            {
                Console.WriteLine($"User Name or ID: {reader["USER_NAME"]}, Name: {reader["ACTION_TYPE"]}, Date And Time: {reader["TIME_DATE"]}, Result: {reader["ACTION_RESULT"]}");
            }
            cmd.Connection.Close();
            Console.WriteLine("Press Enter To Return To Main Menu");
            Console.ReadLine();
        }

        private static void ActionRegistery(string USER,string ACTION,string RESULT)
        {
            string TIME = Convert.ToString(DateTime.Now);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(@"Data Source=LAPTOP-BH097B07\MSSQLSERVER01;Initial Catalog=Project01;Integrated Security=True");
            cmd.Connection.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO ACTION (USER_NAME, ACTION_TYPE, TIME_DATE, ACTION_RESULT) VALUES (@USERNAME, @ACTION, @TIME, @RESULT)";
            cmd.Parameters.Add("@USERNAME", USER);
            cmd.Parameters.Add("@ACTION", ACTION);
            cmd.Parameters.Add("@TIME", TIME);
            cmd.Parameters.Add("@RESULT", RESULT);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
