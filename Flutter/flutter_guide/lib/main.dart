import 'package:flutter/material.dart';

import 'question.dart';
import 'answer.dart';

void main() {
  runApp(App());
}

class App extends StatefulWidget {
  @override
  State<StatefulWidget> createState() {
    return _AppState();
  }
}

class _AppState extends State<App> {
  var _questionIndex = 0;

  void _questionAnswered() {
    setState(() {
      _questionIndex = _questionIndex + 1;
    });
  }

  @override
  Widget build(BuildContext context) {
    const questions = [
      {
        'questionText': 'What\'s your favorite color?',
        'answers': ['Black', 'Red', 'Green', 'White'],
      },
      {
        'questionText': 'What\'s your favorite animal?',
        'answers': ['Rabbit', 'Snake', 'Elephant', 'Lion'],
      },
      {
        'questionText': 'What\'s your favorite instructor?',
        'answers': ['Max', 'Max', 'Max', 'Max'],
      },
    ];

    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(
          title: Text('Killer Party'),
        ),
        body: Column(
          children: [
            Question(questions[_questionIndex]['questionText']),
            ...(questions[_questionIndex]['answers'] as List<String>).map((answer) {
              return Answer(_questionAnswered, answer);
            }).toList(),
          ],
        ),
      ),
    );
  }
}
