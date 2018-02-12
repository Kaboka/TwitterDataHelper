Welcome to TwitterDataHelper.

To setup the database for this application use the guide providede by Helge in the Assignment 2 description.
This application is written in C#. To run the application go to \TwitterDataHelper\TwitterDataHelper\bin\Debug and run TwitterDataHelper.exe
if you need to change database name and connection string look in the file in the debug folder called ConnectionSettings.txt - default it uses port 27017 and name social_net


This is the results that i found:

How Many Twitter users are in the database?
659774

Which Twitter users link most to other twitter users?
*lost_dog
*tweetpet
*VioletsCRUK
*what_bugs_u
*tsarnick
*SallytheShizzle
*mcraddictal
*Karen230683
*keza34
*TraceyHewins

Who are the most mentioned Twitter users?
This part i could not get to work, i found it kinda hard in C#
But in my mind i guess you should do something like this:
*Find all text that include @ using regex "@\\w+"
*Split on @ to get the user names and put them in a field.
*Group the name and count how many times a name show up.
But please tell me if there is a better solution :)


Who are the most active twitter users?
*lost_dog
*webwoke
*tweetpet
*SallytheShizzle
*Violetscruk
*mcraddictal
*tsarnick
*what_bugs_u
*Karen230683
*DarkPiano

Who are the 5 most grumpy?
*lost_dog
*tweetpet
*webwoke
*mcraddictal
*wowlew

Who are the 5 most Happy?
*what_bugs_u
*DarkPiano
*VioletsCRUK
*tsarnick
*keza34