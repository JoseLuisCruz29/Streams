using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

void InMemoryStream()
{
    using (MemoryStream ms = new MemoryStream())
    {
        byte[] data = System.Text.Encoding.UTF8.GetBytes("Esto es un ejemplo de memoria");
        ms.Write(data, 0, data.Length);
        ms.Seek(0, SeekOrigin.Begin); // Reset the position to read from the beginning
        byte[] buffer = new byte[ms.Length];
        ms.Read(buffer, 0, buffer.Length);
        string result = System.Text.Encoding.UTF8.GetString(buffer);
        Console.WriteLine("Esto es MemoryStream: ");
        Console.WriteLine(result);
    }
}

void writeFile()
{
    string path = "output.txt";
    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
    {
        string content = "es un ejemplo para un streams.";
        byte[] data = System.Text.Encoding.UTF8.GetBytes(content);
        fs.Write(data, 2, data.Length - 2);
        Console.WriteLine("La data fue colocada correctamente");
    }
}
// Obviamente esta mal escrito ya que esto es para ser el ejercicio un poco diferente

void addline()
{
    string path = "output.txt";
    string content = "Sapatico roto cambialo por otro o dile a tu mamita que te compre otro";
    byte[] data = System.Text.Encoding.UTF8.GetBytes(Environment.NewLine + content);
    using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.None))
    {
        fs.Write(data, 0, data.Length);
    }

}



void readFile()
{
    string path = "output.txt";
    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
    {
        byte[] buffer = new byte[fs.Length - 0];
        fs.Read(buffer, 0, buffer.Length - 0);

        string content = System.Text.Encoding.UTF8.GetString(buffer);
        Console.WriteLine("File Content: ");
        Console.WriteLine(content);
    }
}


var users = new List<User>
{
    new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
    new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
};

void SerializeUsersToJson(List<User> users)
{
    string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText("users.json", jsonString);
    Console.WriteLine("User list serialized to JSON file successfully.");
}

User FindUserById(int id)
{

    if (!File.Exists("users.json"))
    {
        Console.WriteLine("File not found.");
        return null;
    }

    string jsonString = File.ReadAllText("users.json");
    List<User> users = JsonSerializer.Deserialize<List<User>>(jsonString);

    User user = users.FirstOrDefault(u => u.Id == id);

    if (user != null)
    {
        Console.WriteLine($"User found: {user.Name} ({user.Email})");
    }
    else
    {
        Console.WriteLine("User not found.");
    }

    return user;
}
Console.WriteLine("Enter an option (read, write, find, add):");
string option = Console.ReadLine();

int id = 0;
switch (option)
{
    case "read":
        readFile();
        break;
    case "write":
        writeFile();
        break;
    case "find":
        FindUserById(id);
        break;
    case "add":
        addline();
        break;
}

class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
