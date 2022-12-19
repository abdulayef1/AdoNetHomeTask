using AdoNetHomeTask.Entities;
using AdoNetHomeTask.Exceptions;
using System.Data.SqlClient;

string connectionString = @"Server=DESKTOP-G9OODMI\MSSQLSERVER01;Database=Department;Trusted_Connection=True";

void CreateWorker(Worker worker)
{


    using (SqlConnection connection = new(connectionString))
    {
        connection.Open();
        string query = $"INSERT INTO Workers Values('{worker.Name}','{worker.Surname}','{worker.Salary}')";
        SqlCommand command = new SqlCommand(query, connection);
        int result = command.ExecuteNonQuery();
        Console.WriteLine($"{result} workers created");
    }

}

List<Worker> GetAllWorkers(){ 

    List<Worker> workers = new List<Worker>();

    using (SqlConnection connection=new(connectionString))
    {      
        connection.Open();
        string query = "SELECT * FROM WORKERS";
        SqlCommand command = new(query,connection);
        SqlDataReader reader=command.ExecuteReader();
        
        while (reader.Read())
        {
            Worker worker = new Worker();
            worker.Id =(int) reader["Id"];
            worker.Name =(string) reader["Name"];
            worker.Surname =(string) reader["Surname"];
            worker.Salary =(int) reader["Salary"];
            workers.Add(worker);
        }
    }
    
    return workers; 
}

Worker GetWorkerById(int id)
{

    Worker worker=null;

    using (SqlConnection connection = new(connectionString))
    {
        connection.Open();
        string query = $"SELECT * FROM WORKERS Where Id={id}";
        SqlCommand command = new(query, connection);
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            worker = new Worker();
            worker.Id = (int)reader["Id"];
            worker.Name = (string)reader["Name"];
            worker.Surname = (string)reader["Surname"];
            worker.Salary = (int)reader["Salary"];
            return worker;
        }
    }
    throw new NotFoundException("Worker is not founded !");
}

List<Worker> SearchWorkersByName(string name)
{

    List<Worker> workers = new List<Worker>();

    using (SqlConnection connection = new(connectionString))
    {
        connection.Open();
        string query = $"SELECT * FROM WORKERS where name={name}";
        SqlCommand command = new(query, connection);
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Worker worker = new Worker();
            worker.Id = (int)reader["Id"];
            worker.Name = (string)reader["Name"];
            worker.Surname = (string)reader["Surname"];
            worker.Salary = (int)reader["Salary"];
            workers.Add(worker);
        }
    }

    return workers;
}


#region Testing

//Worker w1 = new();
//w1.Name = "Mahammad";
//w1.Surname = "Abdullayev";
//w1.Salary = 1000;

//CreateWorker(w1);

//foreach (var item in GetAllWorkers())
//{
//    Console.WriteLine(item.Name);
//}

//Console.WriteLine(GetWorkerById(3).Surname);

//foreach (var item in SearchWorkersByName("Mahammad"))
//{
//    Console.WriteLine(item.Id);
//}
#endregion