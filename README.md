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
* Run several sync jobs at the same time, each in its own tab with its own progress and log.
* Provides a text box with RoboCopy commands associated with the changes to the properties.
* Display immediate hints for an option highlighted in the property grid or in the text editor.
* The ability to run the RoboCopy within the GUI.


# Screenshots

**Sync jobs**: every job in the profile, with its source, target and options.

![Main window listing four sync jobs](screenshots/main-window.png "Sync jobs")

**Sync progress, Overview tab**: the whole run at the top (files, speed, elapsed time, time left, errors, jobs done), and every job with its status, progress, files, amount copied, speed and errors. Double-click a job to open its tab.

![Progress window, Overview tab, with several jobs running in parallel](screenshots/sync-progress.png "Sync progress: Overview")

**Sync progress, job tab**: one tab per job, with its own progress bar, the folder and file being copied, and that job's log. Tab titles show each job's state at a glance (`Photos · 45%`, `· waiting`, `· done`, `· failed`).

![Progress window, tab of one job](screenshots/sync-progress-job.png "Sync progress: one job")

**Edit sync job**

![Edit sync job dialog](screenshots/edit-job.png "Edit sync job")

**Advanced job options**: attribute filters, excluded files and folders, age and size limits, mirror mode and NTFS security.

![Advanced job options dialog](screenshots/job-options.png "Advanced job options")

**Profile options**: retries, copy threads, parallel jobs, scan and logging, and programs to run before or after a sync.

![Profile options dialog](screenshots/profile-options.png "Profile options")

# Parallel jobs

A profile's jobs run at the same time, up to **Parallel jobs** (Profile options, default 4); the rest wait and start as soon as a running job finishes. Each job runs its own robocopy, so jobs on different disks or servers finish much sooner together than one after the other. Set it to 1 to run jobs one after the other, for example when they all read from the same hard disk. Jobs whose target folders overlap (one inside the other) always run one after the other, so they can't get in each other's way. Turned-off jobs get no tab and don't run.

If a job fails, the others carry on, and the failures are listed together when the run ends. Stopping the run stops every running job and doesn't start the waiting ones. Shadow copies and the programs to run before and after a sync happen once for the whole run.

# Network outages

If a job's source or target stops answering (network, NAS or share down), RoboG stops that job's robocopy and shows **Waiting for connection**. It checks every 30 seconds and resumes as soon as the share is back; robocopy skips what was already copied, so the job carries on where it stopped. Jobs that start during an outage wait the same way instead of failing. Files that failed because of the outage aren't counted as errors, since they're tried again. If the connection isn't back within 60 minutes, the job fails with a clear message and the other jobs carry on.

This keeps **Retries** and **Wait time** in Profile options for problems with individual files (locked or access denied), so they can stay low without an outage turning into thousands of errors.

# Large jobs (millions to billions of files)

RoboG streams robocopy's output instead of holding it in memory, so memory use stays flat however many files a job has, and parallel jobs don't add up to more than a few megabytes each. The progress window reads each job's counters twice a second and redraws only the tab you are looking at, so the copy never waits for the screen. For the fastest runs, open **Edit > Advanced settings**:

* **Copy threads** (default 16) runs robocopy with `/MT`. 16 to 32 suits most jobs; very small files over a network can benefit from more. Set it to 1 only if you need EFS-encrypted files copied in raw mode (`/EFSRAW` cannot be combined with `/MT`).
* **Scan first** does a list-only pass to find the total size, which gives an accurate progress bar and time estimate but means walking the whole tree twice. Turn it off for very large trees.
* **Show live progress** lists every file in the log. Turning it off (`/NFL /NDL`) is fastest; errors and robocopy's summary are still logged, and each job's files and bytes copied appear when it finishes.
* **Parallel jobs** (default 4) sets how many jobs run at the same time. Each one also uses the copy threads above, so 4 jobs at 16 threads read up to 64 files at once.

Logs are written as the run goes, to `%AppData%\RoboG` (**Open log folder** in the progress window):

* `RoboG-Log.txt`: the run as a whole, such as shadow copies and when each job starts and ends.
* `Jobs\Job-NN-<name>.txt`: everything robocopy reported for that job.
* `RoboG-Errors.txt`: every file that could not be copied, from all jobs, tagged with the job's name.

# Shadow copies (VSS)

With "Use shadow copies" on, RoboG snapshots all source volumes at the same instant with the Volume Shadow Copy Service built into Windows, so open and locked files are copied as they were at that moment. Snapshots are released automatically when the job ends, even if RoboG is closed or crashes. RoboG declares a "copy" backup, so applications such as SQL Server or Exchange don't treat the sync as one of their backups. Shadow copies need RoboG to run as administrator (it offers to restart elevated) and a local NTFS or ReFS source drive. If Windows discards a snapshot during a very long job because shadow storage filled up, RoboG reports it rather than leaving you with unexplained file errors.

# Building

Requires Windows and Visual Studio 2022 or later (Community is fine) with the **.NET desktop development** workload, which includes the .NET Framework 4.8 targeting pack. Open `RoboG.sln`, choose **Release** / **Any CPU** and build; the program is in `bin\Release`. Keep the platform on Any CPU: Windows doesn't allow a 32-bit program to create shadow copies on 64-bit Windows. There are no third-party dependencies.
