﻿{Source_to_Standard}
SELECT distinct SOURCE_CODE, TARGET_CONCEPT_ID
FROM CTE_VOCAB_MAP
WHERE lower(SOURCE_VOCABULARY_ID) = 'icd10' AND lower(TARGET_DOMAIN_ID) = 'observation' AND (TARGET_INVALID_REASON is NULL or TARGET_INVALID_REASON = '')