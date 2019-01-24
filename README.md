# Iceland Gilded Rose Solution
This is my solution to the technical assesment for Iceland's .NET Developer role. I have built it as a .NET Core Console application, that can be built or ran using Visual Studio in the normal way. I decided to use XUnit for testing, in order to ensure that the specification was being accurately adhered to. These tests can be run from the Test Explorer in Visual Studio.

At the moment it runs on the command line, allowing the user to either copy-paste or manually type the inventory that requires updating, however I have left the code open to the addition of other input methods such as file input or using command line arguments.

I decided to use inheritance and polymorphism to specify the types of items and change how the addition of a day affects each. This was done with the aim to allow more types of item to be added with relative ease, only requiring an additional change to the switch statement in InventoryManager.cs.

In order to parse the input lines as strings into Item objects, I used regular expressions working under the assumption that no digits would appear in the item name section. If that was required in future then it's likely that some kind of delimiter would have to be used between the name, sell in and quality.
