import requests
import pandas as pd


def download_api_data(url, api_key, output_file):
    headers = {'X-CoinAPI-Key': api_key}
    response = requests.get(url, headers=headers)

    result = response.json()

    data = pd.DataFrame(result)
    data.to_csv(output_file, index=False)
