import 'package:clima/services/location.dart';
import 'package:clima/services/networking.dart';

const apiKey = '036ad39ce1fbe3dec5660bea79900945';
const openWeatherMapUrl = 'https://api.openweathermap.org/data/2.5/weather';

class WeatherModel {

  Future<dynamic> getCityWeather(String cityName) async {
    var url = '$openWeatherMapUrl?q=$cityName&appid=$apiKey&units=metric';
    var networkHelper = NetworkHelper(url);
    return await networkHelper.getData();
  }

  Future<dynamic> getLocationWeather() async {
    var location = Location();
    await location.getLocation();
    
    var networkHelper = NetworkHelper('$openWeatherMapUrl?lat=${location.latitude}&lon=${location.longitude}&appid=$apiKey&units=metric');
    return await networkHelper.getData();
  }

  String getWeatherIcon(int condition) {
    if (condition < 300) {
      return '🌩';
    } else if (condition < 400) {
      return '🌧';
    } else if (condition < 600) {
      return '☔️';
    } else if (condition < 700) {
      return '☃️';
    } else if (condition < 800) {
      return '🌫';
    } else if (condition == 800) {
      return '☀️';
    } else if (condition <= 804) {
      return '☁️';
    } else {
      return '🤷‍';
    }
  } 

  String getMessage(int temp) {
    if (temp >= 25) {
      return "C'est l'heure de la 🍦";
    } else if (temp >= 18) {
      return 'Sortez le short et le 👕';
    } else if (temp < 10) {
      return "N'oubliez pas 🧣 et 🧤";
    } else {
      return 'Prenez une 🧥 au cas où';
    }
  }
}
