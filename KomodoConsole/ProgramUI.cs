using KomodoRepository;
using System;
using System.Collections.Generic;

namespace KomodoConsole
{
    public class ProgramUI
    {
        private readonly DeveloperRepo _developerRepo = new DeveloperRepo();
        private readonly DevTeamRepo _devTeamRepo = new DevTeamRepo();

        public void Run()
        {
            SeedContentList();
            Menu();
        }

        private void SeedContentList()
        {
            _devTeamRepo.Add(new DevTeam("Web", 1));
            _devTeamRepo.Add(new DevTeam("Android", 2));
            _devTeamRepo.Add(new DevTeam("iOS", 3));

            _developerRepo.Add(new Developer("Skye", "Byrne", 7169, false));
            _developerRepo.Add(new Developer("Keely", "Irvine", 5250, false));
            _developerRepo.Add(new Developer("Kavita", "Cherry", 1527, true));
            _developerRepo.Add(new Developer("Fatema", "Leonard", 4237, true));
            _developerRepo.Add(new Developer("Aaisha", "Corona", 9686, false));
            _developerRepo.Add(new Developer("Tulisa", "Wiggins", 4058, true));
            _developerRepo.Add(new Developer("Poppie", "Rowe", 6749, false));
            _developerRepo.Add(new Developer("Miles", "Ferry", 5569, false));
            _developerRepo.Add(new Developer("Amara", "Schmitt", 9814, true));
            _developerRepo.Add(new Developer("Lillian", "Watts", 1857, false));
        }

        private void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("Select an option:\n" +
                    "1.View all developers\n" +
                    "2.Add a new developer\n" +
                    "3.Update an existing developer\n" +
                    "4.Remove an existing developer\n" +
                    "5.View all teams\n" +
                    "6.Add a new team\n" +
                    "7.Update an existing team\n" +
                    "8.Delete an existing team\n" +
                    "9.View all developers without PluralSight Access\n" +
                    "10.Exit\n");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ViewAllDevelopers();
                        break;
                    case "2":
                        CreateNewDeveloper();
                        break;
                    case "3":
                        UpdateExisitingDeveloper();
                        break;
                    case "4":
                        DeleteExistingDeveloper();
                        break;
                    case "5":
                        ViewAllTeams();
                        break;
                    case "6":
                        CreateNewDevTeam();
                        break;
                    case "7":
                        UpdateExistingTeam();
                        break;
                    case "8":
                        DeleteExistingTeam();
                        break;
                    case "9":
                        ViewAllDevelopersWithoutPluralSight();
                        break;
                    case "10":
                        keepRunning = false;
                        continue;  // Starts the while loop over to skip Console methods at end of Menu
                    default:
                        Console.WriteLine("Choose an available option");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void ViewAllDevelopers()
        {
            Console.Clear();
            PrintAllDevelopers();
        }

        private void PrintAllDevelopers()
        {
            foreach (var developer in _developerRepo.GetAll())
            {
                PrintDeveloper(developer);
            }
        }

        private void PrintDeveloper(Developer developer)
        {
            Console.WriteLine($"{developer.ID} - {developer.FirstName} {developer.LastName} - HasPluralSight: {developer.HasPluralsight}");
        }

        private void CreateNewDeveloper()
        {
            Console.Clear();

            var developer = CreateDeveloperFromUser();
            bool wasAdded = _developerRepo.Add(developer);

            Console.WriteLine();
            if (wasAdded)
            {
                Console.WriteLine("Developer sucessfully created!");
                PrintDeveloper(developer);
            }
            else
            {
                Console.WriteLine($"Developer with ID: {developer.ID} already exists!");
            }
        }

        private Developer CreateDeveloperFromUser()
        {
            Console.Write("Enter the first name of the developer: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter the last name of the developer: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter the ID of the developer: ");
            string idString = Console.ReadLine();
            int id = int.Parse(idString);

            Console.Write("Does this developer have PluralSight Access? (Y/N): ");
            string hasPluralSightInput = Console.ReadLine().ToLower();
            bool hasPluralSight = hasPluralSightInput == "y";

            return new Developer(firstName, lastName, id, hasPluralSight);
        }

        private void UpdateExisitingDeveloper()
        {
            ViewAllDevelopers();
            Console.WriteLine();
            Console.Write("Enter the ID number of the developer you wish to update: ");

            string input = Console.ReadLine();
            if (input == "")
            {
                Console.WriteLine();
                Console.WriteLine($"Nothing was entered!");
                return;
            }

            int id = int.Parse(input);
            var developer = _developerRepo.Get(id);

            if (developer == null)
            {
                Console.WriteLine();
                Console.WriteLine($"A developer with ID: {id} does not exist!");
                return;
            }

            Console.Clear();
            Console.WriteLine("Original Developer Properties");
            PrintDeveloper(developer);
            Console.WriteLine();

            Console.Write("Update the first name of the developer: ");
            developer.FirstName = Console.ReadLine();

            Console.Write("Update the last name of the developer: ");
            developer.LastName = Console.ReadLine();

            Console.Write("Does this developer have PluralSight Access? (Y/N): ");
            string pluralSightAccess = Console.ReadLine().ToLower();
            developer.HasPluralsight = pluralSightAccess == "y";

            Console.WriteLine();
            Console.WriteLine("Developer was updated successfully!");
            PrintDeveloper(developer);
        }

        private void DeleteExistingDeveloper()
        {
            ViewAllDevelopers();
            Console.WriteLine();
            Console.Write("Enter the ID of the developer you wish to remove: ");

            string input = Console.ReadLine();
            if (input == "")
            {
                Console.WriteLine();
                Console.WriteLine($"Nothing was entered!");
                return;
            }

            int id = int.Parse(input);
            var developerToDelete = _developerRepo.Get(id);

            Console.WriteLine();

            if (developerToDelete == null)
            {
                Console.WriteLine($"A developer with ID: {id} does not exist!");
            }
            else
            {
                _developerRepo.Remove(developerToDelete);
                _devTeamRepo.RemoveDeveloperFromAllTeams(developerToDelete);
                PrintDeveloper(developerToDelete);
                Console.WriteLine("The developer was successfully removed!");
            }
        }

        private void ViewAllTeams()
        {
            Console.Clear();
            foreach (var devTeam in _devTeamRepo.GetAll())
            {
                PrintTeamWithMembers(devTeam);
                Console.WriteLine();
            }
        }

        private void PrintTeamWithMembers(DevTeam devTeam)
        {
            PrintTeam(devTeam);
            List<Developer> developers = devTeam.GetDevelopers();

            if (developers.Count == 0)
            {
                Console.WriteLine("<no team members>");
            }
            else
            {
                foreach (var developer in developers)
                {
                    PrintDeveloper(developer);
                }
            }
        }

        private void PrintTeam(DevTeam devTeam)
        {
            Console.WriteLine($"Team ID: {devTeam.ID} - {devTeam.Name}");
        }

        private void CreateNewDevTeam()
        {
            Console.Clear();

            Console.Write("Enter the name of Developer Team: ");
            string teamName = Console.ReadLine();

            Console.Write("Enter the Team ID: ");
            string input = Console.ReadLine();
            int teamID = int.Parse(input);

            var devTeam = new DevTeam(teamName, teamID);

            bool wasAdded = _devTeamRepo.Add(devTeam);
            if (wasAdded)
            {
                AddDevelopersToTeam(devTeam);
            }
            else
            {
                Console.WriteLine($"DevTeam with ID: {teamID} already exists!");
            }
        }

        private void UpdateExistingTeam()
        {
            ViewAllTeams();
            Console.WriteLine();
            Console.Write("Enter the ID number of the team you wish to update: ");

            string input = Console.ReadLine();
            if (input == "")
            {
                Console.WriteLine();
                Console.WriteLine($"Nothing was entered!");
                return;
            }

            int id = int.Parse(input);
            var devTeam = _devTeamRepo.Get(id);

            if (devTeam == null)
            {
                Console.WriteLine();
                Console.WriteLine($"No Team with ID: {id}");
            }
            else
            {
                UpdateExistingTeamMenu(devTeam);
            }
        }

        private void UpdateExistingTeamMenu(DevTeam devTeam)
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine($"Select an option for Team {devTeam.Name}:\n" +
                    "1.View Team Members\n" +
                    "2.Change Team Name\n" +
                    "3.Add Developers to Team\n" +
                    "4.Remove Developers from Team\n" +
                    "5.Return to Main Menu\n");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ViewTeamMembers(devTeam);
                        break;
                    case "2":
                        UpdateTeamName(devTeam);
                        break;
                    case "3":
                        AddDevelopersToTeam(devTeam);
                        break;
                    case "4":
                        RemoveDevelopersFromTeam(devTeam);
                        break;
                    case "5":
                        keepRunning = false;
                        continue;  // Starts the while loop over to skip Console methods at end of Menu
                    default:
                        Console.WriteLine("Choose an available option");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void ViewTeamMembers(DevTeam devTeam)
        {
            Console.Clear();
            PrintTeamWithMembers(devTeam);
        }

        private void UpdateTeamName(DevTeam devTeam)
        {
            Console.Clear();
            PrintTeam(devTeam);
            Console.Write($"Enter the new name of the team: ");
            string teamName = Console.ReadLine();
            devTeam.Name = teamName;

            Console.WriteLine();
            PrintTeam(devTeam);
            Console.WriteLine("Team name sucessfully changed!");
        }

        private void AddDevelopersToTeam(DevTeam devTeam)
        {
            ViewTeamMembers(devTeam);
            Console.WriteLine();
            Console.WriteLine("All Developers");
            PrintAllDevelopers();
            Console.WriteLine();
            Console.Write("Add Developers to team by their ID separated by comma: ");
            string userInput = Console.ReadLine();
            if (userInput == "")
            {
                Console.WriteLine();
                Console.WriteLine($"Nothing was entered!");
                return;
            }
            string[] splitInput = userInput.Split(',');

            Console.WriteLine();
            foreach (string input in splitInput)
            {
                int id = int.Parse(input);
                var developer = _developerRepo.Get(id);  // We don't need to check for null Developers here

                bool wasAdded = devTeam.Add(developer);

                if (wasAdded)
                {
                    Console.WriteLine($"ID: {id} added to the team");
                }
                else if (developer == null)
                {
                    Console.WriteLine($"ID: {id} does not exist!");
                }
                else
                {
                    Console.WriteLine($"ID: {id} was already on the team!");
                }
            }
        }

        private void RemoveDevelopersFromTeam(DevTeam devTeam)
        {
            ViewTeamMembers(devTeam);
            Console.WriteLine();
            Console.Write("Remove Developers from team by their ID separated by comma: ");
            string userInput = Console.ReadLine();
            if (userInput == "")
            {
                Console.WriteLine();
                Console.WriteLine($"Nothing was entered!");
                return;
            }
            string[] splitInput = userInput.Split(',');

            Console.WriteLine();
            foreach (string input in splitInput)
            {
                int id = int.Parse(input);
                var developer = _developerRepo.Get(id);  // We don't need to check for null Developers here

                bool result = devTeam.Remove(developer);

                if (result)
                {
                    Console.WriteLine($"ID: {id} removed from the team");
                }
                else
                {
                    Console.WriteLine($"ID: {id} was already not in the team!");
                }
            }
        }

        private void DeleteExistingTeam()
        {
            ViewAllTeams();
            Console.WriteLine();
            Console.Write("Enter the ID number of the team you wish to delete: ");

            string input = Console.ReadLine();
            if (input == "")
            {
                Console.WriteLine();
                Console.WriteLine($"Nothing was entered!");
                return;
            }
            int id = int.Parse(input);

            bool result = _devTeamRepo.Remove(id);

            Console.WriteLine();
            if (result)
            {
                Console.WriteLine($"Team with ID: {id} sucessfully deleted!");
            }
            else
            {
                Console.WriteLine($"No Team with ID: {id}");
            }
        }

        private void ViewAllDevelopersWithoutPluralSight()
        {
            Console.Clear();
            List<Developer> list = _developerRepo.GetAllDevelopersWithoutPluralSight();

            if (list.Count == 0)
            {
                Console.WriteLine("Everyone has access to PluralSight!");
            }
            else
            {
                foreach (var developer in list)
                {
                    PrintDeveloper(developer);
                }
            }
        }
    }
}