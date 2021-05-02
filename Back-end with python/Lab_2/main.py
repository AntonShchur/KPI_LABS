import re

text = 'With best regards John, S. With best regards Toni, S.'

pattern = '[A-Za-z]+, [A-Za-z]'
all_matches = re.findall(pattern=pattern, string=text)

print(all_matches)
