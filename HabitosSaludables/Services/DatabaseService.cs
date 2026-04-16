using SQLite;
using HabitosSaludables.Models;

namespace HabitosSaludables.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Habito>().Wait();
            _database.CreateTableAsync<User>().Wait();  // 👈 Crear tabla de usuarios
        }

        // ========== MÉTODOS PARA HÁBITOS ==========
        public Task<List<Habito>> GetHabitosAsync()
            => _database.Table<Habito>().ToListAsync();

        public Task<int> SaveHabitoAsync(Habito habito)
        {
            if (habito.Id != 0)
                return _database.UpdateAsync(habito);
            else
                return _database.InsertAsync(habito);
        }

        public Task<int> DeleteHabitoAsync(Habito habito)
            => _database.DeleteAsync(habito);

        public async Task<bool> ToggleCompletadoAsync(Habito habito)
        {
            if (habito.CompletadoHoy) return false;

            var today = DateTime.Now.ToString("yyyy-MM-dd");
            var lastCheck = habito.UltimoCheck;

            if (string.IsNullOrEmpty(lastCheck))
            {
                habito.RachaActual = 1;
            }
            else
            {
                var yesterday = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                if (lastCheck == yesterday)
                    habito.RachaActual++;
                else if (lastCheck != today)
                    habito.RachaActual = 1;
            }

            if (habito.RachaActual > habito.MejorRacha)
                habito.MejorRacha = habito.RachaActual;

            habito.CompletadoHoy = true;
            habito.UltimoCheck = today;

            await _database.UpdateAsync(habito);
            return true;
        }

        public async Task ResetDailyCompletions()
        {
            var habitos = await GetHabitosAsync();
            var today = DateTime.Now.ToString("yyyy-MM-dd");
            foreach (var h in habitos)
            {
                if (h.UltimoCheck != today)
                {
                    h.CompletadoHoy = false;
                    await _database.UpdateAsync(h);
                }
            }
        }

        // ========== MÉTODOS PARA USUARIOS ==========
        public Task<User> GetUserByEmail(string email)
            => _database.Table<User>().FirstOrDefaultAsync(u => u.Email == email);

        public Task<int> RegisterUser(User user)
            => _database.InsertAsync(user);
    }
}