﻿using System;

namespace ZuulCS
{
	public class Game
	{
        
        private Parser parser;
        private Player player;

        internal Player _player { get => player; }


        public Game ()
		{
            player = new Player();
            parser = new Parser();

            createRooms();
        }

        public void createRooms()
        {
            Room outside, hallway, storage, restroom, operationroom;

            // create the rooms
            outside = new Room("outside the main entrance of the hospital");
            hallway = new Room("in a hallway");
            storage = new Room("in the storage room");
            restroom = new Room("in a restroom");
            operationroom = new Room("in a operation room");

            // initialise room exits
            outside.setExit("east", hallway);

            hallway.setExit("east", operationroom);
            hallway.setExit("south", restroom);
            hallway.setExit("west", outside);

            operationroom.setExit("up", storage);
            operationroom.setExit("west", hallway);

            storage.setExit("down", operationroom);

            restroom.setExit("north", hallway);

            operationroom.Inventory.addItem(new UsedNeedle("used needle", "Can be used for lock picking maybe"));


            player.CurrentRoom = operationroom;  // start game in operationroom


        }

        /**
	     *  Main play routine.  Loops until end of play.
	     */
        public void play()
		{
			printWelcome();
            


            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.
            bool finished = false;
			while (! finished) {
				Command command = parser.getCommand();
				finished = processCommand(command);
			}
			Console.WriteLine("Thank you for playing.");
		}

		/**
	     * Print out the opening message for the player.
	     */
		private void printWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Zuul!");
			Console.WriteLine("Zuul is a new, incredibly creepy adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
            printHelp();
            Console.WriteLine("----------------------------------------------------------)");
            Console.WriteLine(player.CurrentRoom.getLongDescription());
            
        }

		/**
	     * Given a command, process (that is: execute) the command.
	     * If this command ends the game, true is returned, otherwise false is
	     * returned.
	     */
		private bool processCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.isUnknown()) {
				Console.WriteLine("I don't know what you mean...");
				return false;
			}

			string commandWord = command.getCommandWord();
			switch (commandWord) {

				case "help":
					printHelp();
					break;

				case "go":
					goRoom(command);
                    Console.WriteLine("items: ");
                    insideInventory();
                    break;

				case "quit":
					wantToQuit = true;
					break;

                case "look":
                    looked();
                    Console.WriteLine("items: ");
                    insideInventory();
                    break;

                case "take":
                    takeItem(command);
                    break;

                case "drop":
                    break;

            }

			return wantToQuit;
		}

		// implementations of user commands:

		/**
	     * Print out some help information.
	     * Here we print some stupid, cryptic message and a list of the
	     * command words.
	     */
		private void printHelp()
		{
            Console.WriteLine("----------------------------------------------------------)");
            Console.WriteLine("You are lost. You are alone (aleast for now..)");
			Console.WriteLine("You wander around at a abandoned hospital");
            Console.WriteLine("try to escape the building");
            Console.WriteLine();
			Console.WriteLine("Your command words are:");
			parser.showCommands();
            
        }

		/**
	     * Try to go to one direction. If there is an exit, enter the new
	     * room, otherwise print an error message.
	     */
		private void goRoom(Command command)
		{
			if(!command.hasSecondWord()) {
				// if there is no second word, we don't know where to go...
				Console.WriteLine("Go where?");
				return;
			}

			string direction = command.getSecondWord();

			// Try to leave current room.
			Room nextRoom = player.CurrentRoom.getExit(direction);

			if (nextRoom == null) {
				Console.WriteLine("There is no door to "+direction+"!");
			} else {
				player.CurrentRoom = nextRoom;
                Console.WriteLine();
                Console.WriteLine(player.CurrentRoom.getLongDescription());
                Console.WriteLine("                                             your health is: "+player.damage(1));
                player.isAlive();
			}
		}
        private void looked()
        {
            Console.WriteLine();
            Console.WriteLine(player.CurrentRoom.getLongDescription());
        }

        public void insideInventory()
        {
            for (int i = 0; i < player.CurrentRoom.Inventory.Items.Count; i++)
            {
                
                Console.WriteLine(player.CurrentRoom.Inventory.Items[i].Name + " - " + player.CurrentRoom.Inventory.Items[i].getDescription);

            }
        }


        private void takeItem(Command command)
        {
            if (!command.hasSecondWord())
            {
                // if there is no second word, we don't know what to pick up...
                Console.WriteLine("pickup what?");
                return;
            }

            //player.CurrentRoom.Inventory.Items[i].Name = command.hasSecondWord();

            //player.CurrentRoom.Inventory.Items = command.getSecondWord();

            if (player.CurrentRoom.Inventory.Items == null)
            {
                Console.WriteLine("There is no item " + player.CurrentRoom + "!");
            }
            else if(player.Inventory.WeightLeft > 0)
            {
                player.CurrentRoom.Inventory.takeItem(player.Inventory, command.getSecondWord());
                Console.WriteLine();
                Console.WriteLine("you picked up an item!");
            }
        }
    }
}
