import 'dart:math';

class Calculator {
  final int height;
  final int weight;

  double _bmi;

  Calculator({this.height, this.weight});

  String calculateBmi() {
    _bmi = weight / pow(height / 100, 2);
    return _bmi.toStringAsFixed(1);
  }

  String getResult() {
    if (_bmi > 40) return 'Obésité massive';
    else if (_bmi > 35) return 'Obésité sévère';
    else if (_bmi > 30) return 'Obésité modérée';
    else if (_bmi > 25) return 'Surpoids';
    else if (_bmi >= 18.5) return 'Poids idéal';
    else if (_bmi > 16.5) return 'Maigreur';
    else return 'Anorexie';
  }

  String getInterpretation() {
    if (_bmi > 25) return 'Vous avez un poids supérieur à la normale. Essayez de faire plus d\'exercices physiques.';
    else if (_bmi >= 18.5) return 'Vous avez un poids normal. Bon travail !';
    else return 'Vous avez un poids inférieur à la normale. Vous pouvez manger un peu plus !';
  }
}
