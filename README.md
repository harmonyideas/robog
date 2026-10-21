RoboG
=====
Tired of the complexities of RoboCopy? Introducing RoboG, a revolutionary GUI designed to make using RoboCopy easier than ever. No more cryptic commands or confusing options!

Mirror files with confidence. RoboG simplifies the process of creating full mirror copies of your data. Simply select source and destination folders, and RoboG handles the rest, ensuring all subdirectories and files are replicated flawlessly.

Preserving your data's integrity is paramount. Unlike basic copy tools, RoboG meticulously transfers all file attributes, including timestamps, permissions, and more. This ensures your data remains pristine and unaltered.

Unleash the power of RoboCopy without the learning curve. Whether you're a tech novice or just prefer a user-friendly interface, RoboG is your perfect companion. Its intuitive design makes even the most complex file replication tasks manageable.

Experience the power of RoboG:

Effortless mirroring: Create full mirror copies of your data with a few clicks.
Data integrity: Preserves timestamps, permissions, and other vital file attributes.
User-friendly interface: Simple and intuitive design for seamless file replication.
Unleash RoboCopy's power: Access RoboCopy's advanced features without the command-line hassle.

* Support VSS/Shadow copy volume service
* Simple and intuitive GUI.
* Compose and Save the options as a profile for reuse.
* Run multiple instances of robocopy jobs or save them as scripts to run in a cmd window.
* Provides a text box with RoboCopy commands associated with the changes to the properties.
* Display immediate hints for an option highlighted in the property grid or in the text editor.
* The ability to run the RoboCopy within the GUI.


# Large jobs (millions to billions of files)

RoboG streams robocopy's output instead of holding it in memory, so memory use stays flat however many files a job has. For the fastest runs, open **Edit > Advanced settings**:

* **Copy threads** (default 16) runs robocopy with `/MT`. 16 to 32 suits most jobs; very small files over a network can benefit from more. Set it to 1 only if you need EFS-encrypted files copied in raw mode (`/EFSRAW` cannot be combined with `/MT`).
* **Scan first** does a list-only pass to find the total size, which gives an accurate progress bar and time estimate but means walking the whole tree twice. Turn it off for very large trees.
* **Show live progress** lists every file in the log. Turning it off (`/NFL /NDL`) is fastest; errors and robocopy's summary are still logged.

The full log is written as the job runs to `%AppData%\RoboG\RoboG-Log.txt`, and errors to `RoboG-Errors.txt` in the same folder.

# Shadow copies (VSS)

With "Use shadow copies" on, RoboG snapshots all source volumes at the same instant with the Volume Shadow Copy Service built into Windows, so open and locked files are copied as they were at that moment. Snapshots are released automatically when the job ends, even if RoboG is closed or crashes. RoboG declares a "copy" backup, so applications such as SQL Server or Exchange don't treat the sync as one of their backups. Shadow copies need RoboG to run as administrator (it offers to restart elevated) and a local NTFS or ReFS source drive. If Windows discards a snapshot during a very long job because shadow storage filled up, RoboG reports it rather than leaving you with unexplained file errors.

# Building

Requires Windows and Visual Studio 2022 or later (Community is fine) with the **.NET desktop development** workload, which includes the .NET Framework 4.8 targeting pack. Open `RoboG.sln`, choose **Release** / **Any CPU** and build; the program is in `bin\Release`. Keep the platform on Any CPU: Windows doesn't allow a 32-bit program to create shadow copies on 64-bit Windows. There are no third-party dependencies.

# Screenshots
![robog-screenshot1](/screenshots/robog-screenshot1.png "Main Form")
![robog-screenshot2](/screenshots/robog-screenshot2.png "Advanced Backup Settings")
![robog-screenshot3](/screenshots/robog-screenshot3.png "Advanced Settings")
![robog-screenshot3](/screenshots/robog-screenshot4.png "Status Window")


