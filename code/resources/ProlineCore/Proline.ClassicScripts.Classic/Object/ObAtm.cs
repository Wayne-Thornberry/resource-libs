using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.CFXExtended.Core;
using Proline.CFXExtended.Core.Scaleforms;

namespace Proline.ClassicOnline.SClassic.Object
{
    public class ObAtm
    {
        private Atm io;
        private int ticks;
        private Task<int> getSelectionTask;

        public ObAtm()
        {
        }

        public static Entity NearestEntity { get; set; }
        public string[] ValidModels { get; set; }
         
        public Atm Display { get; set; }
        public int SelectedAmount { get; set; }
        public int DisplayedView { get; set; }
        public bool IsDepositing { get; set; }
        public int ScriptStage { get; set; }
        public int CashMultiplier { get; set; }
        public int SelectedOption { get; private set; }
         
        private int selectedAmount;
        private const int MAX_LIMIT = 10000;

        public int[] CashAmounts { get; set; }

        public async Task Execute(object[] args, CancellationToken token)
        {
            var entityHandle = (int)args[0];
            var entity = Entity.FromHandle(entityHandle);
            ValidModels = new[] { "prop_atm_01", "prop_atm_02", "prop_atm_03", "prop_fleeca_atm" }; 
            CashAmounts = new[] { 100, 200, 500, 1000, 5000, MAX_LIMIT };
            DisplayedView = 0;
            ScriptStage = 9;

            while (ScriptStage != -1 && entity.Exists() && true)
            {

                if (ScriptStage == 9)
                {
                    if (Game.IsControlJustPressed(0, Control.Context))
                    {
                        ScriptStage = 0;
                    }
                }
                else
                {
                    io = new Atm();
                    await io.Load();
                    switch (ScriptStage)
                    {
                        case 0:
                            Game.DisableAllControlsThisFrame(0);
                            if (ticks == 0)
                            {
                                io.DisplayBalance(Game.Player.Name,
                                    "Account Balance: ",
                                   (int)MGame.MGameAPI.GetCharacterBankBalance());
                                RenderMainPage();
                            }

                            ticks++;
                            if (ticks > 250)
                            {
                                ScriptStage = 1;
                            }
                            break;
                        case 1:
                            io.ShowCursor(true);
                            API.ShowCursorThisFrame();
                            //io.SetCursorState(1);
                            io.SetMouseInput(Game.GetControlNormal(0, Control.CursorX), Game.GetControlNormal(0, Control.CursorY));
                            //MDebugAPI.LogDebugLine(Game.GetControlNormal(0, Control.CursorX) + " " + Game.GetControlNormal(0, Control.CursorY));


                            if (DisplayedView == 5)
                            {
                                ticks++;
                                if (ticks > 250)
                                {
                                    RenderTransactionComplete();
                                }
                            }


                            if (getSelectionTask != null)
                            {
                                if (getSelectionTask.IsCompleted)
                                {
                                    SelectedOption = getSelectionTask.Result;
                                    getSelectionTask.Dispose();
                                    getSelectionTask = null;

                                    //MDebugAPI.LogDebug(DisplayedView.ToString());
                                    //MDebugAPI.LogDebug(SelectedOption.ToString());

                                    switch (DisplayedView)
                                    {
                                        // Main Menu
                                        case 0:
                                            switch (SelectedOption)
                                            {
                                                case 1:
                                                    IsDepositing = false;
                                                    RenderCashPage("Select the amount you wish to withdraw from this account", CashAmounts);
                                                    break;
                                                case 2:
                                                    IsDepositing = true;
                                                    RenderCashPage("Select the amount you wish to deposit into this account", CashAmounts);
                                                    break;
                                                case 3:
                                                    RenderTranactionsPage();
                                                    break;
                                            }

                                            break;
                                        // Cash Selection
                                        case 1:
                                            selectedAmount = 0;
                                            switch (SelectedOption)
                                            {
                                                case 1:
                                                    selectedAmount = CashAmounts[0];
                                                    break;
                                                case 2:
                                                    selectedAmount = CashAmounts[1];
                                                    break;
                                                case 3:
                                                    selectedAmount = CashAmounts[2];
                                                    break;
                                                case 4:
                                                    RenderMainPage();
                                                    break;
                                                case 5:
                                                    selectedAmount = CashAmounts[3];
                                                    break;
                                                case 6:
                                                    selectedAmount = CashAmounts[4];
                                                    break;
                                                case 7:
                                                    selectedAmount = CashAmounts[5];
                                                    break;
                                                case 8:
                                                    CashMultiplier++;
                                                    RenderCashPage("Select the amount you wish to deposit into this account", MultiplyArray(CashAmounts, CashMultiplier));
                                                    break;
                                            }

                                            if (SelectedOption != 4 && SelectedOption != 8)
                                            {
                                                RenderConfirmationPage(selectedAmount, IsDepositing);
                                            }
                                            break;
                                        case 2:
                                            switch (SelectedOption)
                                            {
                                                case 1:
                                                    RenderProcessPage();
                                                    break;
                                                case 2:
                                                    RenderMainPage();
                                                    break;
                                            }
                                            break;
                                        case 3:
                                            if (SelectedOption == 1)
                                            {
                                                RenderMainPage();
                                            }
                                            break;
                                        case 4:
                                            if (SelectedOption == 2)
                                            {
                                                RenderMainPage();
                                            }
                                            else
                                            {
                                                RenderProcessPage();
                                            }
                                            break;
                                        case 6:
                                            if (SelectedOption == 1)
                                            {
                                                RenderMainPage();
                                            }
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (Game.IsControlJustPressed(0, Control.FrontendAccept) || Game.IsControlJustPressed(0, Control.Attack))
                                {
                                    io.SetInputSelect();
                                    io.DisplayBalance(Game.Player.Name,
                                        "Account Balance: ",
                                         (int)MGame.MGameAPI.GetCharacterBankBalance());
                                    getSelectionTask = io.GetCurrentSelection();
                                }
                                else if (Game.IsControlJustPressed(0, Control.FrontendCancel))
                                {
                                    io.SetInputSelect();
                                    switch (DisplayedView)
                                    {
                                        case 0:
                                            ScriptStage = 3;
                                            break;
                                        default:
                                            RenderMainPage();
                                            break;
                                    }
                                }
                                else if (Game.IsControlJustPressed(0, Control.FrontendUp))
                                {
                                    io.SetInputEvent(1);
                                }
                                else if (Game.IsControlJustPressed(0, Control.FrontendRight))
                                {
                                    io.SetInputEvent(2);
                                }
                                else if (Game.IsControlJustPressed(0, Control.FrontendDown))
                                {
                                    io.SetInputEvent(3);
                                }
                                else if (Game.IsControlJustPressed(0, Control.FrontendLeft))
                                {
                                    io.SetInputEvent(4);
                                }
                            }

                            break;
                        case 3:
                            ScriptStage = -1;
                            break;
                    }
                    if (ScriptStage != 3 || ScriptStage != -1)
                    {
                        io.Render2D();
                    }
                }

                await BaseScript.Delay(0);
            }
            io.Dispose();
        }

        private int[] MultiplyArray(int[] cashAmounts, int v)
        {
            var arr = new int[cashAmounts.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = cashAmounts[i] * v;
            }
            return arr;
        }

        private void RenderTransactionComplete()
        {

            DisplayedView = 6;
            io.SetDataSlotEmpty();
            io.SetDataSlot(0, "Done");
            io.SetDataSlot(1, "CONFIRM");
            io.DisplayMessage();
        }

        private void RenderProcessPage()
        {
            DisplayedView = 5;
            ticks = 0;
            io.SetDataSlotEmpty();
            io.SetDataSlot(0, "Processing Amount");
            if (IsDepositing)
            { 
                MGame.MGameAPI.SetCharacterBankBalance(MGame.MGameAPI.GetCharacterBankBalance() + selectedAmount);
                MGame.MGameAPI.SetCharacterWalletBalance(MGame.MGameAPI.GetCharacterWalletBalance() - selectedAmount);
            }
            else
            {
                MGame.MGameAPI.SetCharacterWalletBalance(MGame.MGameAPI.GetCharacterWalletBalance() + selectedAmount);
                MGame.MGameAPI.SetCharacterBankBalance(MGame.MGameAPI.GetCharacterBankBalance() - selectedAmount); 
            }
            io.DisplayMessage();
        }

        private void RenderTranactionsPage()
        {
            DisplayedView = 3;
            io.SetDataSlot(0, "$1000000");
            io.SetDataSlot(1, "Back");
            for (int i = 2; i < 10; i++)
            {
                io.SetDataSlot(i, "This is a test string");
            }
            io.DisplayTransactions();
        }

        private void RenderMainPage()
        {
            DisplayedView = 0;
            io.SetDataSlotEmpty();
            io.SetDataSlot(0, "Please choose an option below");
            io.SetDataSlot(1, "Withdraw");
            io.SetDataSlot(2, "Deposit");
            io.SetDataSlot(3, "Transactions");
            io.DisplayMenu();
        }

        private void RenderConfirmationPage(int selectedAmount, bool isDeposit)
        {
            DisplayedView = 4;
            var title = isDeposit
                ? $"Are you sure you wish to deposit {selectedAmount} into this account"
                : $"Are you sure you wish to withdraw {selectedAmount} from this account";
            io.SetDataSlotEmpty();
            io.SetDataSlot(0, title);
            io.SetDataSlot(1, "CONFIRM");
            io.SetDataSlot(2, "DENY");
            io.DisplayMessage();
        }

        private void RenderCashPage(string title, int[] cashAmounts)
        {
            DisplayedView = 1;
            var max = IsDepositing ? MGame.MGameAPI.GetCharacterWalletBalance() : MGame.MGameAPI.GetCharacterBankBalance();
            cashAmounts[5] = (int)max;//(max > MAX_LIMIT ? MAX_LIMIT : max);
            io.DisplayBalance(Game.Player.Name, "Account Balance:",
                (int)MGame.MGameAPI.GetCharacterBankBalance());
            io.SetDataSlotEmpty();
            io.SetDataSlot(0, title);
            io.SetDataSlot(1, cashAmounts[0]);
            io.SetDataSlot(2, cashAmounts[1]);
            io.SetDataSlot(3, cashAmounts[2]);
            io.SetDataSlot(4, "BACK");
            io.SetDataSlot(5, cashAmounts[3]);
            io.SetDataSlot(6, cashAmounts[4]);
            io.SetDataSlot(7, cashAmounts[5]);
            io.SetDataSlot(8, "NEXT");
            io.DisplayCashOptions();
        }
    }
}
