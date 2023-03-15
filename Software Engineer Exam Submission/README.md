Unity Version 2021.3.8f1

System Setup
    The main controller and model of the system is the SlotLogic script
    It contains the data for reels, symbols, lines, and payouts
    It also contains the functions needed for getting reel targets
    and computing resulting scores
    The ReelManager manages the views with the use of Reel objects
    Both of these main scripts can be found attached to the MainPanel object

Data Sources
    All the data sources are found in the SlotLogic script, and can be edited there directly
    The reel, symbol, and line data can be edited

Scalability
    The Scalability of the system is hindered by the number of reels, as increasing them
    would require an overhaul of the UI system.
    Creating more lines or symbols may still be difficult but is doable

Flexibility
    Since the ReelManager generates the reels on startup, the reel data can be edited with
    no issues.
    The same can be said of the line and symbol data

Future Improvements
    As of current submission, the project lacks several main specifications, which are the following:
        Info button that triggers a popup showing line payouts
        Remote configuration
    There are also issues within the code itself, which can be done much cleaner, specifically how
    arrays are instantiated. Applying better coding practices would remove much of the redundancy found in 
    the major scripts.