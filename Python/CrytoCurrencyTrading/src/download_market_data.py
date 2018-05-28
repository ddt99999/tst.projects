import requests
import numpy as np
import pandas as pd

import utility as util

api_key = '49BDA6DB-987A-4528-9CBA-47A625C075EB'

# download exchange data
# util.download_api_data('https://rest.coinapi.io/v1/exchanges', api_key, '..//data//Exchanges.csv')

# download all assets code
# util.download_api_data('https://rest.coinapi.io/v1/assets', api_key, '..//data//Assets.csv')

# download all symbols
# util.download_api_data('https://rest.coinapi.io/v1/symbols', api_key, '..//data//Symbols.csv')


# download OHLC of BTCUSD
#util.download_api_data('https://rest.coinapi.io/v1/ohlcv/BITSTAMP_SPOT_BTC_USD/history?period_id=1MIN&time_start=2018-04-30T00:00:00&time_end=2018-04-30T23:59:59&limit=3000', api_key, '..//data//BITSTAMP_SPOT_BTC_USD.csv')


url = "https://api.bitfinex.com/v1/book/btcusd"

response = requests.request("GET", url)

print(response.text)

