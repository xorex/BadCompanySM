namespace BCM.Commands
{
  public class BCRemoveQuestFromPlayer : BCCommandAbstract
  {
    public override void Process()
    {
      if (SenderInfo.IsLocalGame)
      {
        SendOutput(@"Use the ""removequest"" command for the local player.");

        return;
      }

      if (Params.Count != 2)
      {
        SendOutput("Invalid arguments");
        SendOutput(GetHelp());

        return;
      }

      var count = ConsoleHelper.ParseParamPartialNameOrId(Params[0], out string _, out ClientInfo clientInfo);
      if (count == 1)
      {
        if (clientInfo == null) return;

        clientInfo.SendPackage(new NetPackageConsoleCmdClient("removequest " + Params[1], true));
      }
      else if (count > 1)
      {
        SendOutput("Multiple matches found: " + count);
      }
      else
      {
        SendOutput("Playername or entity ID not found.");
      }
    }
  }
}
