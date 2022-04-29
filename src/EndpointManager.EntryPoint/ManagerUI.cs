using EndpointManager.Application;
using EndpointManager.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndpointManager.EntryPoint
{
    public static class ManagerUI
    {
        private static Dictionary<int, string> _menuItens { get; set; }
        private static Dictionary<int, string> MenuItens => _menuItens;
        internal static EndPointApplication _endPointApplication { get; set; }
        private static EndPointApplication EndPointApplication => _endPointApplication;
        internal static ModelApplication _modelApplication { get; set; }
        private static ModelApplication ModelApplication => _modelApplication;

        public static async Task Run(EndPointApplication endpoinApplication, ModelApplication modelApplication)
        {
            Console.Clear();
            _menuItens = new Dictionary<int, string>();
            _endPointApplication = endpoinApplication;
            _modelApplication = modelApplication;

            bool continueProcess;

            do
            {
                continueProcess = await ProcessRequest();
            } while (continueProcess);

            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Thank you.");
            Console.WriteLine("");
            PressAnyKey();
        }

        #region ConsoleConfigurations

        #region Menu

        private static async Task<bool> ProcessRequest()
        {
            bool result = true;

            CreateMenu();

            ConsoleKeyInfo UserInput = Console.ReadKey();
            int option = char.IsDigit(UserInput.KeyChar) ? (MenuItens.ContainsKey(int.Parse(UserInput.KeyChar.ToString())) ? int.Parse(UserInput.KeyChar.ToString()) : -1) : -1;
            Console.WriteLine("Processing request....");
            switch (option)
            {
                case 1:
                    Console.Clear();
                    await AddNewEndpoint();
                    PressAnyKey();
                    break;
                case 2:
                    Console.Clear();
                    await EditAnEndpoint();
                    PressAnyKey();
                    break;
                case 3:
                    Console.Clear();
                    await DeletAnEndpointAsync();
                    PressAnyKey();
                    break;
                case 4:
                    Console.Clear();
                    ListAllEndPoint();
                    PressAnyKey();
                    break;
                case 5:
                    Console.Clear();
                    FindAnEndpointBySerialNumber();
                    PressAnyKey();
                    break;
                case 6:
                    Console.Clear();
                    result = ExitOption();
                    break;
                default:
                    Console.Clear();
                    YellowMessage("this option is not valid");
                    break;
            }
            return result;
        }
        private static void CreateMenu()
        {
            if (!MenuItens.Any())
            {
                MenuItens.Add(1, "Insert new Endpoint");
                MenuItens.Add(2, "Edit an endpoint");
                MenuItens.Add(3, "Delete an endpoint");
                MenuItens.Add(4, "List all endpoint");
                MenuItens.Add(5, "Find a endpoint by serial Number");
                MenuItens.Add(6, "Exit");
            }

            PrintMenu();
        }
        private static void PrintMenu()
        {
            Console.WriteLine("Welcome to Endpoint Manager");
            Console.WriteLine("");
            Console.WriteLine("Select one option");
            foreach (var item in MenuItens)
            {
                Console.WriteLine($"{item.Key}) {item.Value}");
            }
        }

        #endregion

        #region Messages

        private static void YellowMessage(string message)
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.WriteLine("");
            Console.ResetColor();
        }
        private static void RedMessage(string message)
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.WriteLine("");
            Console.ResetColor();
        }

        #endregion

        #region KeyInteraction

        private static void PressAnyKey()
        {
            YellowMessage("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }
        private static string ReadAKey(string message)
        {
            Console.WriteLine(message);

            ConsoleKeyInfo UserInput = Console.ReadKey();
            string option = UserInput.KeyChar.ToString().ToLower();

            return option;
        }
        private static bool TryAgainOption()
        {
            Console.WriteLine("do you want to try agan? (y)");

            ConsoleKeyInfo UserInput = Console.ReadKey();
            string option = UserInput.KeyChar.ToString().ToLower();

            return option.Equals("y");
        }

        #endregion

        #region Table
        public static string ToStringTable<T>(this IEnumerable<T> values, string[] columnHeaders, params Func<T, object>[] valueSelectors)
        {
            return ToStringTable(values.ToArray(), columnHeaders, valueSelectors);
        }

        public static string ToStringTable<T>(
          this T[] values,
          string[] columnHeaders,
          params Func<T, object>[] valueSelectors)
        {
            Debug.Assert(columnHeaders.Length == valueSelectors.Length);
            var arrValues = new string[values.Length + 1, valueSelectors.Length];
            // Fill headers
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                arrValues[0, colIndex] = columnHeaders[colIndex];
            }
            // Fill table rows
            for (int rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    arrValues[rowIndex, colIndex] = valueSelectors[colIndex]
                      .Invoke(values[rowIndex - 1]).ToString();
                }
            }
            return ToStringTable(arrValues);
        }
        public static string ToStringTable(this string[,] arrValues)
        {
            int[] maxColumnsWidth = GetMaxColumnsWidth(arrValues);
            var headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);
            var sb = new StringBuilder();
            for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    // Print cell
                    string cell = arrValues[rowIndex, colIndex];
                    cell = cell.PadRight(maxColumnsWidth[colIndex]);
                    sb.Append(" | ");
                    sb.Append(cell);
                }
                // Print end of line
                sb.Append(" | ");
                sb.AppendLine();
                // Print splitter
                if (rowIndex == 0)
                {
                    sb.AppendFormat(" |{0}| ", headerSpliter);
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
        private static int[] GetMaxColumnsWidth(string[,] arrValues)
        {
            var maxColumnsWidth = new int[arrValues.GetLength(1)];
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
                {
                    int newLength = arrValues[rowIndex, colIndex].Length;
                    int oldLength = maxColumnsWidth[colIndex];
                    if (newLength > oldLength)
                    {
                        maxColumnsWidth[colIndex] = newLength;
                    }
                }
            }
            return maxColumnsWidth;
        }
        #endregion

        #endregion

        #region Options

        private static async Task AddNewEndpoint()
        {
            bool repeatOperation = false;
            try
            {
                Console.Clear();
                Console.WriteLine("Add new endpoint");
                Console.WriteLine("");
                if (ReadAKey("To create a new endpoint you will need a model id.\nDo you ant to lis all available models? (y)").Equals("y"))
                {
                    GetAllModel();
                }

                Console.WriteLine("");
                Console.WriteLine("Enter with the endpoint configuration separated by ',' in the the folowing sequence");
                Console.WriteLine("Serial number, Model id, Meter number, Meter Firmware Version, Switch State");
                Console.WriteLine("For Switch State you can use the following options");
                Console.WriteLine("0) Disconnected");
                Console.WriteLine("1) Connected");
                Console.WriteLine("2) Armed");
                Console.WriteLine("");

                var endpoint = (Console.ReadLine()).Trim().Split(",");

                if (endpoint.Count().Equals(5))
                {
                    await EndPointApplication.CreateEndpoint(endpoint);
                }
                else
                {
                    throw new InvalidEndpointParametersException();
                }

                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine($"A endpoint with serial number {endpoint[0]} was created successfully");
                Console.WriteLine("");
            }
            catch (InvalidEndpointParametersException invalidParametersExcepetion)
            {
                RedMessage(invalidParametersExcepetion.Message);
                repeatOperation = TryAgainOption();
            }
            catch (ModelNotFindException modelNotFindException)
            {
                RedMessage(modelNotFindException.Message);
                repeatOperation = TryAgainOption();
            }
            catch (EndpointAlreadExistException endpointAlreadExistException)
            {
                RedMessage(endpointAlreadExistException.Message);
                repeatOperation = TryAgainOption();
            }
            catch (Exception)
            {
                RedMessage("unexpected error");
                throw;
            }

            if (repeatOperation) await AddNewEndpoint();
        }
        private static async Task EditAnEndpoint()
        {
            bool repeatOperation = false;
            try
            {
                Console.Clear();
                Console.WriteLine("Edit a endpoint");
                Console.WriteLine("");
                Console.WriteLine("You can only edit the Switch status");
                Console.WriteLine("Enter with the endpoint serial number to edit");
                var serialNumber = (Console.ReadLine()).Trim();

                Console.WriteLine("");
                Console.WriteLine("For Switch State you can use the following options");
                Console.WriteLine("0) Disconnected");
                Console.WriteLine("1) Connected");
                Console.WriteLine("2) Armed");
                var newSwithState = ReadAKey("Enter with the new switch state");

                await EndPointApplication.EditEndPointState(serialNumber, newSwithState);

                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine($"A endpoint with serial number {serialNumber} was changed to Swith state {newSwithState}");
                Console.WriteLine("");
            }
            catch (SwitchStateIsNotValidException invalidParametersExcepetion)
            {
                RedMessage(invalidParametersExcepetion.Message);
                repeatOperation = TryAgainOption();
            }
            catch (EndpointNotFindException modelNotFindException)
            {
                RedMessage(modelNotFindException.Message);
                repeatOperation = TryAgainOption();
            }
            catch (Exception)
            {
                RedMessage("unexpected error");
                throw;
            }

            if (repeatOperation) await EditAnEndpoint();
        }
        private static async Task DeletAnEndpointAsync()
        {
            bool repeatOperation = false;
            try
            {
                Console.Clear();
                Console.WriteLine("Delete an endpoint");
                Console.WriteLine("");
                Console.WriteLine("Enter with the endpoint serial number to edit");
                var serialNumber = (Console.ReadLine()).Trim();

                await EndPointApplication.DeleteEndpointBySerialNumberAsync(serialNumber);

                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine($"A endpoint with serial number {serialNumber} was deleted");
                Console.WriteLine("");
            }
            catch (EndpointNotFindException modelNotFindException)
            {
                RedMessage(modelNotFindException.Message);
                repeatOperation = TryAgainOption();
            }
            catch (Exception)
            {
                RedMessage("unexpected error");
                throw;
            }

            if (repeatOperation) await DeletAnEndpointAsync();
        }
        private static void ListAllEndPoint()
        {
            try
            {
                var endpoints = EndPointApplication.GetAllEndpoints();

                List<Tuple<string, string, int, string, string>> modelsList = new List<Tuple<string, string, int, string, string>>();

                foreach (var item in endpoints)
                {
                    modelsList.Add(Tuple.Create(item.SeriaNumber, $"Id: {item.Model.Id} -> Code: {item.Model.Code}", item.Number, item.FirmwareVersion, item.State.ToString()));
                }

                PrintEndpoint(modelsList);
            }
            catch (Exception)
            {
                RedMessage("unexpected error");
            }
        }
        private static void FindAnEndpointBySerialNumber()
        {
            bool repeatOperation = false;
            try
            {
                Console.Clear();
                Console.WriteLine("Find an endpoint");
                Console.WriteLine("");
                Console.WriteLine("Enter with the endpoint serial number to find");
                var serialNumber = (Console.ReadLine()).Trim();

                var endpoint = EndPointApplication.GetBySerialNumber(serialNumber);

                if (endpoint is null)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Endpoint with the serial number {serialNumber} was not found");
                    repeatOperation = TryAgainOption();
                }
                else
                {
                    PrintEndpoint(new List<Tuple<string, string, int, string, string>>() {
                        new Tuple<string, string, int, string, string>(endpoint.SeriaNumber, $"Id: {endpoint.Model.Id} -> Code: {endpoint.Model.Code}", endpoint.Number, endpoint.FirmwareVersion, endpoint.State.ToString())
                    });
                }
                
            }
            catch (Exception)
            {
                RedMessage("unexpected error");
            }

            if (repeatOperation) FindAnEndpointBySerialNumber();
        }
        private static bool ExitOption()
        {
            Console.Clear();
            var result = ProcessExit().Equals("n");
            Console.Clear();
            return result;
        }
        private static string ProcessExit()
        {
            string option = ReadAKey("Are you sure you want to exit ? (y)/(n)");

            if (!option.Equals("y") && !option.Equals("n"))
            {
                Console.Clear();
                YellowMessage("this option is not valid");
                option = ProcessExit();
            }
            return option;
        }
        private static void PrintEndpoint(List<Tuple<string, string, int, string, string>> modelsList)
        {
            Console.WriteLine($" -> Total models: {modelsList.Count()}");

            Console.WriteLine(modelsList.ToStringTable(
              new[] { "Serial Number", "Model", "Number", "Firmware version", "State" },
              a => a.Item1, a => a.Item2, a => a.Item3, a => a.Item4, a => a.Item5));

            Console.WriteLine($" -> Total models: {modelsList.Count()}");
        }
        private static void GetAllModel()
        {
            try
            {
                var models = ModelApplication.GetAllModels();

                List<Tuple<int, string>> modelsList = new List<Tuple<int, string>>();

                foreach (var item in models)
                {
                    modelsList.Add(Tuple.Create(item.Id, item.Code));
                }

                Console.WriteLine($" -> Total models: {models.Count()}");

                Console.WriteLine(modelsList.ToStringTable(
                  new[] { "Id", "Model" },
                  a => a.Item1, a => a.Item2));

                Console.WriteLine($" -> Total models: {models.Count()}");
            }
            catch (Exception)
            {
                RedMessage("unexpected error");
            }
        }

        #endregion
    }
}
