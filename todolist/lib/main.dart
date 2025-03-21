import 'package:flutter/material.dart';
import 'package:collection/collection.dart';

class Todo {
  String? task;
  String? priority;
  bool? isDone;
  IconData? icon;

  Todo() {
    task = "";
    priority = "";
    isDone = false;
    icon = Icons.check_box_outline_blank;
  }

  int comparator() {
    if (priority == "High") {
      return 0;
    } else if (priority == "Medium") {
      return 1;
    } else {
      return 2;
    }
  }
}

List<Todo> todos = [];

typedef PriorityEntry = DropdownMenuEntry<PriorityLabel>;

enum PriorityLabel {
  red("High", Colors.red),
  yellow("Medium", Colors.yellow),
  green("Low", Colors.green);

  const PriorityLabel(this.label, this.color);
  final String label;
  final Color color;

  static final List<PriorityEntry> entries = UnmodifiableListView<PriorityEntry>(
    values.map<PriorityEntry>(
      (PriorityLabel color) => PriorityEntry(
        value: color,
        label: color.label,
        enabled: color.label != 'Grey',
        style: MenuItemButton.styleFrom(foregroundColor: color.color),
      ),
    ),
  );
}

typedef IconEntry = DropdownMenuEntry<IconLabel>;

enum IconLabel {
  work('Work', Icons.work),
  home('Private', Icons.home),
  school('School', Icons.school);

  const IconLabel(this.label, this.icon);
  final String label;
  final IconData icon;

  static final List<IconEntry> entries = UnmodifiableListView<IconEntry>(
    values.map<IconEntry>(
      (IconLabel icon) => IconEntry(
        value: icon,
        label: icon.label,
        leadingIcon: Icon(icon.icon),
      ),
    ),
  );
}

void main() {
  runApp(MyApp());
}


class TodoController {
  void addTask(String task, String priority, IconData icon) {
    Todo todo = Todo();
    todo.task = task;
    todo.priority = priority;
    todo.icon = icon;
    todos.add(todo);
  }

  void removeTaskAtIndex(int index) {
    todos.removeAt(index);
  }

  void removeTask(String task) {
    todos.removeWhere((element) => element.task == task);
  }

  void sortTasks() {
    todos.sort((a, b) => a.comparator().compareTo(b.comparator()));
  }
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: const ToDoPage(),
    );
  }
}

class TodoListScreen extends StatefulWidget {
  const TodoListScreen({super.key});

  @override
  State<TodoListScreen> createState() => _TodoListScreenState();
}

class _TodoListScreenState extends State<TodoListScreen> {
  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(8.0),
      child: Center(
        child: Material(
          child: Directionality(
            textDirection: TextDirection.rtl,
            child: ListView(
              children: [
                for (var todo in todos)
                  ListTile(
                    title: Text(todo.task ?? "No task specified"),
                    textColor: (todo.priority == "High") 
                        ? Colors.red 
                        : (todo.priority == "Medium") 
                            ? Colors.yellow 
                            : Colors.green,
                    leading: Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Icon(todo.icon ?? Icons.help_outline),
                    ),

                    trailing: Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Checkbox(
                        value: todo.isDone ?? false,
                        onChanged: (value) {
                          setState(() {
                            todos.removeAt(todos.indexOf(todo));
                            TodoController().sortTasks();
                          });
                        },
                      ),
                    ),
                  ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}


class ToDoPage extends StatefulWidget {
  const ToDoPage({super.key});

  @override
  State<ToDoPage> createState() => _ToDoPageState();
}

class _ToDoPageState extends State<ToDoPage> {
  final TextEditingController colorController = TextEditingController();
  final TextEditingController iconController = TextEditingController();
  PriorityLabel? selectedPriority;
  IconLabel? selectedIcon;
  String? taskDescription;
  
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(colorSchemeSeed: const Color.fromARGB(255, 8, 247, 40)),
      debugShowCheckedModeBanner: false,
      home: Scaffold(
        appBar: AppBar(
          centerTitle: true,
          title: const Text('Todo List'),
        ),
        body: SafeArea(
          child: Column(
            children: <Widget>[
              Center(
                child: Padding(
                  padding: const EdgeInsets.symmetric(vertical: 20),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: <Widget>[
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: SizedBox(
                          width: 200,
                          child: TextFormField(
                            decoration: const InputDecoration(
                              hintText: 'Enter task description',
                            ),
                            onChanged: (value) {
                              taskDescription = value;
                            },
                          ),
                        ),
                      ),
                      DropdownMenu<PriorityLabel>(
                        initialSelection: PriorityLabel.green,
                        controller: colorController,
                        requestFocusOnTap: true,
                        label: const Text('Priority'),
                        onSelected: (PriorityLabel? color) {
                          setState(() {
                            selectedPriority = color;
                          });
                        },
                        dropdownMenuEntries: PriorityLabel.entries,
                      ),
                      const SizedBox(width: 24),
                      DropdownMenu<IconLabel>(
                        initialSelection: IconLabel.home,
                        controller: iconController,
                        requestFocusOnTap: true,
                        label: const Text('Icon'),
                        onSelected: (IconLabel? icon) {
                          setState(() {
                            selectedIcon = icon;
                          });
                        },
                        dropdownMenuEntries: IconLabel.entries,
                      ),
                      Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: ElevatedButton(
                          style: ElevatedButton.styleFrom(
                            shape: RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(10),
                            ),
                          ),
                          onPressed: () {
                            if (taskDescription != null && selectedPriority != null && selectedIcon != null) {
                              TodoController().addTask(taskDescription!, selectedPriority!.label, selectedIcon!.icon);
                              TodoController().sortTasks();
                              setState(() {
                                taskDescription = null;
                                selectedPriority = null;
                                selectedIcon = null;
                              });
                            }
                          },
                          child: const Text('Add Task'),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
              if (selectedPriority != null && selectedIcon != null)
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: <Widget>[
                    Text(
                      'You selected ${selectedPriority?.label} priority and ${selectedIcon?.label}',
                    ),
                    Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 5),
                      child: Icon(
                        selectedIcon?.icon,
                        color: selectedPriority?.color,
                      ),
                    ),
                  ],
                )
              else
                const Text('Please select a priority and an icon.'),
              Expanded(
                child: TodoListScreen(),
              ),
            ], 
          ),
        ),
      ),
    );
  }
}
